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
    public class RentalService : IRentalService
    {
        private readonly IRentalsRepository rentalsRepository;

        public RentalService(IRentalsRepository rentalsRepository)
        {
            this.rentalsRepository = rentalsRepository;
        }

        public void Create(Rentals entity)
        {
            rentalsRepository.CreateNewRental(entity);
        }

        public void Delete(Guid? id)
        {
            rentalsRepository.DeleteRental(id);
        }

        public List<Rentals> getAllRentals()
        {
            return rentalsRepository.getAllRentals().ToList();
        }

        public Rentals GetRental(Guid? id)
        {
            return rentalsRepository.getRentalById(id);
        }


    }
}
