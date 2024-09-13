using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class Car : BaseEntity
    {
        public CarBrand Brand { get; set; }

        public int PricePerDay { get; set; }

        public string? ImageURL { get; set; }
    }
}
