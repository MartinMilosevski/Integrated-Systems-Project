using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly IRepository<CarBrand> carBrandService;

        public CarService(ICarRepository carRepository, IRepository<CarBrand> carBrandService)
        {
            this.carRepository = carRepository;
            this.carBrandService = carBrandService;
        }

        public void CreateCar(Car car)
        {
            carRepository.CreateCar(car);
        }

        public void DeleteCar(Guid? carId)
        {
            Car car = carRepository.getCarById(carId);
            carBrandService.Delete(car.Brand.Id);
        }

        public List<Car> GetAllCars()
        {
            return carRepository.getCars().ToList();
        }

        public Car GetCar(Guid? carId)
        {
            return carRepository.getCarById(carId);
        }

        public void UpdateCar(Car car)
        {
            carRepository.UpdateCar(car);
        }
    }
}
