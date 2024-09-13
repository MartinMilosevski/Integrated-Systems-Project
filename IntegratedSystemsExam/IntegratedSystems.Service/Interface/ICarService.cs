using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCar(Guid? carId);
        void CreateCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(Guid? carId);
    }
}
