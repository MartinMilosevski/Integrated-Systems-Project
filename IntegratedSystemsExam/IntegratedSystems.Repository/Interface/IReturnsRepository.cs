using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Interface
{
    public interface IReturnsRepository
    {
        IEnumerable<Returns> getReturns();
        Returns GetReturn(BaseEntity id);
        void CreateReturns(Returns returns);
        void UpdateReturns(Returns returns);
        void DeleteReturns(Guid? id);
    }
}
