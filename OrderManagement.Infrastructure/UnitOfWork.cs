using OrderManagement.Contracts.Data;
using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Infrastructure.Repositories.Generic;
using OrderManagement.Contracts.Entities;
using OrderManagement.Contracts.Data.Cache;

namespace OrderManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SouthWestTradersDbContext _context;
        private readonly IDistributedCacheRepository _distributedCacheRepository;
        public UnitOfWork(SouthWestTradersDbContext context, IDistributedCacheRepository distributedCacheRepository)
        {
            _context = context; 
            _distributedCacheRepository = distributedCacheRepository;
        }
        public IOrderRepository Order => new OrderRepository(_context);

        public IProductRepository Product => new ProductRepository(_context);

        public IStockRepository Stock => new StockRepository(_context); 

        public IOrderStateRepository OrderState => new OrderStateRepository(_context, _distributedCacheRepository);  

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
