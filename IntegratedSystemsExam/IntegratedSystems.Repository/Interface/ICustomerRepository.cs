using IntegratedSystems.Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Interface
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();

        Customer GetCustomerById(string? Id);

        void Update(Customer customer);

    }
}
