using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Infrastructure.Repositories.Generic;
using OrderManagement.Contracts.Entities;

namespace OrderManagement.Infrastructure.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(SouthWestTradersDbContext context)
            : base(context)
        {
        }
    }
}
