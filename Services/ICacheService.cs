namespace Redis.CachingService.Services
{
    public interface ICacheService
    {
        Task<T?> GetCachedDataAsync<T>(CacheCategory category, string identifier);

        Task SetCachedDataAsync<T>(CacheCategory category, string cacheKey, T data, TimeSpan? expiry = null);
        Task RemoveCachedDataAsync(CacheCategory category, string cacheKey);
        void ValidateCacheKey(string cacheKey);
    }
}