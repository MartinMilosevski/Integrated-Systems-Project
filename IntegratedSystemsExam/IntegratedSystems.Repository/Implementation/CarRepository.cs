using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Car> _cars;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
            _cars = _context.Set<Car>();
        }


        public void CreateCar(Car car)
        {
            _cars.Add(car);
            _context.SaveChanges();
        }

        public void DeleteCar(Guid? carId)
        {
            _cars.Remove(_cars.FirstOrDefault(u=>u.Id == carId));
            _context.SaveChanges();
        }

        public Car getCarById(Guid? carId)
        {
            return _cars.
                Include(u=>u.Brand)
                .FirstOrDefault(u=>u.Id==carId);
        }

        public IEnumerable<Car> getCars()
        {
           return _cars.Include(u => u.Brand).ToList();
        }

        public void UpdateCar(Car car)
        {
            _cars.Update(car);
            _context.SaveChanges();
        }
    }
}
