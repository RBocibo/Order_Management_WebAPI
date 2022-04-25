using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Contracts.Entities;

namespace OrderManagement.Infrastructure.Repositories.Generic
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SouthWestTradersDbContext context)
            : base(context)
        {          
        }
    }
}
