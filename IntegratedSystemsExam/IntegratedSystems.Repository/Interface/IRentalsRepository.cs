using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Interface
{
    public interface IRentalsRepository
    {
        IEnumerable<Rentals> getAllRentals();
        Rentals getRentalById(Guid? id);
        void CreateNewRental(Rentals newRental);
        void DeleteRental(Guid? id);
    }
}
