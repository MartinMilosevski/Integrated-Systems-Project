using IntegratedSystems.Domain.Identity_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Customer> _customerSet;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
            _customerSet=context.Set<Customer>();
        }


        public IEnumerable<Customer> GetAll()
        {
            return _customerSet.ToList();
        }

        public Customer GetCustomerById(string? Id)
        {
            return _customerSet
                .Include(z=>z.rentals)
                .Include("rentals.car")
                .Include("rentals.car.Brand")
                .FirstOrDefault(u => u.Id == Id);
        }

        public void Update(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            _customerSet.Update(customer);
            _context.SaveChanges();
        }
    }
}
