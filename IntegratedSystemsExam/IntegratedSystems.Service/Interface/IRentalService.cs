using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IRentalService
    {
        List<Rentals> getAllRentals();

        Rentals GetRental(Guid? id);

        void Create(Rentals entity);

        void Delete(Guid? id);
    }
}
