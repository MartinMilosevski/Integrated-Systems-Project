using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedSystems.Web.Controllers
{
    public class Car : Controller
    {
        private readonly ICarService carService;

        public Car(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()
        {
            return View(carService.GetAllCars());
        }


        public IActionResult CarDetails(Guid? id)
        {
            return View(carService.GetCar(id));
        }


        public IActionResult CreateNewCarPage()
        {
            return View();      
        }


        public IActionResult CreateCar(IntegratedSystems.Domain.Domain_Models.Car car)
        {
            carService.CreateCar(car);
            return RedirectToAction("Index");
        }

        public IActionResult CarDelete(Guid? id)
        {
            carService.DeleteCar(id);
            return RedirectToAction("Index");
        }


    }
}
