using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Distributed;

namespace Redis.CachingService.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        // Define a naming convention pattern (e.g., "ContextType-queryparams")
        // The cache key should start with a letter, followed by a hyphen, and then a combination of letters and numbers
         private const string CacheKeyPattern = @"^[a-zA-Z]+(-[a-zA-Z0-9]+)+$";

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        // Generate a cache key using the enum and identifier
        public string GenerateCacheKey(CacheCategory category, string identifier)
        {
            string cacheKey = $"{category}-{identifier}";
            ValidateCacheKey(cacheKey);
            return cacheKey;
        }

        // Get cached data from the cache
        // If the data is not found, return the default value
        public async Task<T?> GetCachedDataAsync<T>(CacheCategory category, string identifier)
        {
            string _cacheKey = GenerateCacheKey(category, identifier);

            var cachedData = await _cache.GetStringAsync(_cacheKey);
            if (string.IsNullOrEmpty(cachedData))
            {

                return default;
            }
            var deserializedData = JsonSerializer.Deserialize<T>(cachedData);
            return deserializedData ?? default;
        }

        public async Task RemoveCachedDataAsync(CacheCategory category, string identifier)
        {
            string cacheKey = GenerateCacheKey(category, identifier);
            await _cache.RemoveAsync(cacheKey);
        }

        public async Task SetCachedDataAsync<T>(CacheCategory category, string identifier, T data, TimeSpan? expiry = null)
        {
            string cacheKey = GenerateCacheKey(category, identifier);

            var serializedData = JsonSerializer.Serialize(data);

            // if the expiry is null, by default set it to 1 hour
            if (expiry == null)
            {
                expiry = TimeSpan.FromHours(1);
            }
            await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry
            });
        }

        // Validate the cache key against the naming convention
        public void ValidateCacheKey(string cacheKey)
        {
            if (!Regex.IsMatch(cacheKey, CacheKeyPattern))
            {
                throw new ArgumentException($"Cache key '{cacheKey}' does not follow the required naming convention.");
            }
        }
    }
}