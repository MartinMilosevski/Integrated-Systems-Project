using Humanizer;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Domain.Identity_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Repository.Migrations;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Stripe;
using Stripe.Issuing;
using System.Security.Claims;

namespace IntegratedSystems.Web.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRentalService rentalService;
        private readonly ICarService carService;
        private readonly ICustomerRepository customerRepository;
        private readonly IReturnsRepository returnsRepository;

        public RentalsController(IRentalService rentalService, ICarService carService,
            ICustomerRepository customerRepository, IReturnsRepository returnsRepository)
        {
            this.rentalService = rentalService;
            this.carService = carService;
            this.customerRepository = customerRepository;
            this.returnsRepository = returnsRepository;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Domain.Identity_Models.Customer customer = customerRepository.GetCustomerById(userId?.ToString());
            if (customer != null)
            {
                return View(customer.rentals?.ToList());
            }
            return Redirect("Identity/Account/Login");
        }

        public IActionResult AddCarToRentalsPage(Guid? id)
        {
            return View(carService.GetCar(id));
        }


        public IActionResult AddCarToRentals(Guid? CarId, DateOnly DFrom, DateOnly DTo)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            Rentals rentals = new Rentals
            {
                CarId = CarId,
                car = carService.GetCar(CarId),
                Date_from = DFrom,
                Date_on = DTo,
                CustomerId = userId,
                customer = customerRepository.GetCustomerById(userId),
                FullPrice = (DTo.DayNumber - DFrom.DayNumber) * carService.GetCar(CarId).PricePerDay
            };
            rentalService.Create(rentals);
            return RedirectToAction("Index", "Car");
        }


        public IActionResult DeleteRental(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString();
            Domain.Identity_Models.Customer customer = customerRepository.GetCustomerById(userId);
            rentalService.Delete(customer.rentals?.FirstOrDefault(u => u.Id == id)?.Id);
            return RedirectToAction("Index");
        }


        public IActionResult DeleteAllRentals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString();
            Domain.Identity_Models.Customer customer = customerRepository.GetCustomerById(userId);
            customer.rentals?.Clear();
            customerRepository.Update(customer);
            return RedirectToAction("Index");
        }

        public IActionResult PayAllRentals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString();
            Domain.Identity_Models.Customer customer = customerRepository.GetCustomerById(userId);
            if (customer.rentals?.Count == 0)
            {
                return RedirectToAction("Index", "Car");
            }

            

            foreach (var item in customer?.rentals)
            {

                returnsRepository.CreateReturns(new Returns
                {
                    Rental=item,
                    RentalId = item.Id,
                    Additional_fee = (int)(item.car.PricePerDay * 0.3),
                    CustomerName=item.customer?.FirstName
                });
                

            }

            customer.rentals.Clear();
            customerRepository.Update(customer);
            return RedirectToAction("Index");
        }

        public IActionResult PayOrder(string StripeEmail,string stripeToken)
        {
            var CustomerService = new CustomerService();
            var chargeService=new ChargeService();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.ToString();
            var order = customerRepository.GetCustomerById(userId);
            var fullprice = 0;
            foreach (var item in order.rentals)
            {
                fullprice += item.FullPrice;
            }

            var customer = CustomerService.Create(new CustomerCreateOptions
            {
                Email = StripeEmail,
                Source=stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount=fullprice,
                 Description="Payment",
                 Currency="usd",
                 Customer=customer.Id
            });

            if (charge.Status == "succeeded")
            {
                order.rentals.Clear();
                customerRepository.Update(order);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Car","Index");

        }


    }
}

