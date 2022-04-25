using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Infrastructure.Repositories.Generic;
using OrderManagement.Contracts.Entities;

namespace OrderManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouthWestTradersDbContext _context;
        public UnitOfWork(SouthWestTradersDbContext context)
        {
            _context = context; 
        }
        public IOrderRepository Order => new OrderRepository(_context);

        public IProductRepository Product => new ProductRepository(_context);

        public IStockRepository Stock => new StockRepository(_context); 

        public IOrderStateRepository OrderState => new OrderStateRepository(_context);  

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
