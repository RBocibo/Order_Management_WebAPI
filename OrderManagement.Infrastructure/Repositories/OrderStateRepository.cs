using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Infrastructure.Repositories.Generic;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderStateRepository : Repository<OrderState>, IOrderStateRepository
    {
        public OrderStateRepository(SouthWestTradersDbContext context)
            : base(context)
        {

        }
    }
}
