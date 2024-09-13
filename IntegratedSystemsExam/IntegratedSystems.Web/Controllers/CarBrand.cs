using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedSystems.Web.Controllers
{
    public class CarBrand : Controller
    {
        private readonly ICarBrandService carBrandService;

        public CarBrand(ICarBrandService carBrandService)
        {
            this.carBrandService = carBrandService;
        }

        public IActionResult Index()
        {
            return View(carBrandService.GetAllBrands());
        }

    }
}
