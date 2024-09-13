using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class CarBrandService : ICarBrandService
    {
        private readonly IRepository<CarBrand> _repository;

        public CarBrandService(IRepository<CarBrand> repository)
        {
            _repository = repository;
        }

        public void CreateCarBrand(CarBrand brand)
        {
            _repository.Insert(brand);
        }

        public void DeleteCarBrand(CarBrand brand)
        {
            _repository.Delete(brand.Id);
        }

        public List<CarBrand> GetAllBrands()
        {
            List<CarBrand> carBrands = new List<CarBrand>();
            
            for(int i = 0; i < _repository.GetAll().Count(); i++)
            {
                bool flag = false;
                for (int j = i+1; j < _repository.GetAll().Count(); j++)
                {
              if (_repository.GetAll().ElementAt(i).Manufacturer == _repository.GetAll().ElementAt(j).Manufacturer)
                    {
                        flag = true;
                        break;
                    }

                }
                if (flag == false)
                {
                    carBrands.Add(_repository.GetAll().ElementAt(i));
                }
            }
            return carBrands.ToList();   
        }

        public void UpdateCarBrand(CarBrand brand)
        {
            _repository.Update(brand);
        }
    }
}
