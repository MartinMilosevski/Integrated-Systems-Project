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
    public class RentalRepository : IRentalsRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Rentals> rentals;

        public RentalRepository(ApplicationDbContext context)
        {
            this.context = context;
            rentals=context.Set<Rentals>();
        }

        public void CreateNewRental(Rentals newRental)
        {
            rentals.Add(newRental);
            context.SaveChanges();
        }

        public void DeleteRental(Guid? id)
        {
            rentals.Remove(rentals.FirstOrDefault(u=>u.Id == id));
            context.SaveChanges();
        }

        public IEnumerable<Rentals> getAllRentals()
        {
            return rentals
                .Include(u=>u.car)
                .Include(u=>u.customer)
                .Include(U=>U.car.Brand)
                .ToList();
        }

        public Rentals getRentalById(Guid? id)
        {
            return rentals
                .Include(u => u.car)
                .Include(U=>U.customer)
                .Include(U => U.car.Brand)
                .FirstOrDefault(u => u.Id == id);
        }
    }
}
