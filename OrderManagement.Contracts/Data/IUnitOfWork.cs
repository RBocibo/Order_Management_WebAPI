using OrderManagement.Contracts.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Contracts.Data
{
    public interface IUnitOfWork
    {
        IOrderRepository Order { get; } 
        IProductRepository Product { get; }
        IStockRepository Stock { get; }
        IOrderStateRepository OrderState { get; }

        Task CommitAsync();

    }
}
