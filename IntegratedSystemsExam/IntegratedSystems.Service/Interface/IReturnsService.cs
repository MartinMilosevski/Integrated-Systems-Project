using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IReturnsService
    {
        List<Returns> GetAllReturns();
        Returns GetReturns(BaseEntity? id);
        void Create(Returns returns);
        void Update(Returns returns);
        void Delete(Returns returns);
    }
}
