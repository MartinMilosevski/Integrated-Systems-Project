using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class CarBrand : BaseEntity
    {
      
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int year { get; set; }
        public double Km { get; set; }

    }
}
