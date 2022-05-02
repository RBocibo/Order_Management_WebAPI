using Microsoft.Extensions.Caching.Distributed;
using OrderManagement.Contracts.Data.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Cache
{
    public class DistributedCacheRepository : IDistributedCacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<string> GetAsync(string cacheKey)
        {
            try
            {
                var cachedValue = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedValue))
                {
                    return cachedValue;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
                throw;
            }
        }

        public async Task PurgeAsync(string cacheKey)
        {
            try
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        public async Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions)
        {
            try
            {
                await _distributedCache.SetStringAsync(cacheKey, cacheValue, cacheEntryOptions);
            }
            catch (Exception)
            {
                // do nothing
            }
        }
    }
}
