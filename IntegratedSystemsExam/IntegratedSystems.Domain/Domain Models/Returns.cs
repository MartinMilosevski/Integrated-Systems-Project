using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Returns : BaseEntity
    {
        public int Additional_fee { get; set; }
        public Guid? RentalId { get; set; }
        public Rentals? Rental { get; set; }
        public string? CustomerName { get; set; }
    }
}
