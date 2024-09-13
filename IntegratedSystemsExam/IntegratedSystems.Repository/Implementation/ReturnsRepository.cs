using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class ReturnsRepository : IReturnsRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Returns> returns;

        public ReturnsRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.returns=context.Set<Returns>();
        }

        public void CreateReturns(Returns returns)
        {
            if (returns == null) { 
                throw new ArgumentNullException(nameof(returns)); 
            }
            this.returns.Add(returns);
            context.SaveChanges();
        }

        public void DeleteReturns(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            returns.Remove(returns.FirstOrDefault(x => x.Id == id));
            context.SaveChanges();
        }

        public Returns GetReturn(BaseEntity? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            return returns
                .Include(z => z.Rental)
                .Include(z => z.Rental.car)
                .Include(z => z.Rental.customer)
                .Include(z => z.Rental.car.Brand)
                .FirstOrDefault(x => x.Id == id.Id);
        }

        public IEnumerable<Returns> getReturns()
        {
            return returns
                .Include(z => z.Rental)
                .Include(z => z.Rental.car)
                .Include(z => z.Rental.customer)
                .Include(z => z.Rental.car.Brand)
                .ToList();
        }

        public void UpdateReturns(Returns returns)
        {
            if (returns == null)
            {
                throw new ArgumentNullException(nameof(returns));
            }
            this.returns.Update(returns);
            context.SaveChanges();
        }
    }
}
