using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
