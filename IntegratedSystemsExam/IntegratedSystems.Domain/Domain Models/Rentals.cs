using IntegratedSystems.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Rentals : BaseEntity
    {
        public Guid? CarId { get; set; }
        public Car? car { get; set; }
        public string? CustomerId { get; set; }
        public Customer? customer { get; set; }
        public DateOnly Date_from { get; set; }
        public DateOnly Date_on { get; set; }
        public int FullPrice { get; set; }

        public Rentals() { }

    }
}
