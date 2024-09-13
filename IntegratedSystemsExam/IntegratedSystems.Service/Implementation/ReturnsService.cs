using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class ReturnsService : IReturnsService
    {
        private readonly IReturnsRepository returnsRepository;

        public ReturnsService(IReturnsRepository returnsRepository)
        {
            this.returnsRepository = returnsRepository;
        }

        public void Create(Returns returns)
        {
            if (returns == null)
            {
                throw new ArgumentNullException(nameof(returns));
            }
            returnsRepository.CreateReturns(returns);
        }

        public void Delete(Returns returns)
        {
            if (returns==null)
            {
                throw new ArgumentNullException(nameof(returns));
            }
            returnsRepository.DeleteReturns(returns.Id);
        }

        public List<Returns> GetAllReturns()
        {
            return returnsRepository.getReturns().ToList();
        }

        public Returns GetReturns(BaseEntity id)
        {
            return returnsRepository.GetReturn(id);
        }

        public void Update(Returns returns)
        {
            if(returns == null)
            {
                throw new ArgumentNullException(nameof(returns));
            }
            returnsRepository.UpdateReturns(returns);
        }
    }
}
