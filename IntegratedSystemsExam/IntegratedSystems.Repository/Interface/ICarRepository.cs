using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Interface
{
    public interface ICarRepository
    {
        IEnumerable<Car> getCars();

        Car getCarById(Guid? carId);

        void CreateCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(Guid? carId);

    }
}
