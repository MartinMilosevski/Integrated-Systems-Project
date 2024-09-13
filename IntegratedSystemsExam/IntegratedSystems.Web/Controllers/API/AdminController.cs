using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IntegratedSystems.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IReturnsService returnsService;
        private readonly ICarService carService;


        public AdminController(IReturnsService returnsService,ICarService carService)
        {
            this.returnsService = returnsService;
            this.carService = carService;
        }

        [HttpGet("[action]")]
        public List<Returns> getReturns()
        {
            return returnsService.GetAllReturns();
        }

        [HttpPost("[action]")]
        public Returns GetReturn(BaseEntity Id)
        {
            return returnsService.GetReturns(Id);
        }


        [HttpPost("[action]")]
        public void ImportAllCars(List<IntegratedSystems.Domain.Domain_Models.Car> cars)
        {
            foreach (var car in cars)
            {
                carService.CreateCar(car);
            }
            
        }


    }
}
