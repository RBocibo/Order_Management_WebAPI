using OrderManagement.Contracts.Data.Repositories;
using OrderManagement.Infrastructure.Repositories.Generic;
using OrderManagement.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Contracts.Data.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderStateRepository : Repository<OrderState>, IOrderStateRepository
    {
        private readonly IDistributedCacheRepository _distributedCacheRepository;
        private readonly string _cacheKey;
        private readonly int _absoluteExpiration;
        public OrderStateRepository(SouthWestTradersDbContext context, IDistributedCacheRepository distributedCacheRepository)
            : base(context)
        {
            _distributedCacheRepository = distributedCacheRepository;
            _cacheKey = "OrderStatus";
            _absoluteExpiration = 5; // hours
        }

        public async Task<IEnumerable<OrderState>> GetCachedOrderStates()
        {
            return await GetCache();
        }

        public async Task<IEnumerable<OrderState>> GetCache()
        {
            var cachedOrderStates = await _distributedCacheRepository.GetAsync(_cacheKey);
            if (!string.IsNullOrEmpty(cachedOrderStates))
            {
                var orderStates = JsonConvert.DeserializeObject<List<OrderState>>(cachedOrderStates);
                return orderStates;
            }
            else
            {
                var dbOrderStates = await ListAsync();//.AsQueryable();

                var jsonOrderStates = JsonConvert.SerializeObject(dbOrderStates);

                var options = new DistributedCacheEntryOptions()
                                  .SetAbsoluteExpiration(TimeSpan.FromHours(_absoluteExpiration));

                await _distributedCacheRepository.SetAsync(_cacheKey, jsonOrderStates, options);

                return dbOrderStates;
            }
        }

        public async Task<OrderState> GetCachedOrderStatesByKey(int orderStateId)
        {
            var orderState = await GetCache();
            return orderState.FirstOrDefault(x => x.OrderStateId == orderStateId);
        }
    }
}
