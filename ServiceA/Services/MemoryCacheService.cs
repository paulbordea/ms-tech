using Microsoft.Extensions.Caching.Memory;

namespace ServiceA.Services
{
    public class MemoryCacheService : IMemoryCacheService<double>
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public double Get(string key)
        {
            _memoryCache.TryGetValue(key, out double averageValue);
            return averageValue;
        }

        public void Set(string key, double value)
        {
            _memoryCache.Set(key, value);
        }
    }
}
