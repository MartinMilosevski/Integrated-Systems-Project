using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface ICarBrandService
    {
        public List<CarBrand> GetAllBrands();

        void CreateCarBrand(CarBrand brand);
        void DeleteCarBrand(CarBrand brand);
        void UpdateCarBrand(CarBrand brand);
    }
}
