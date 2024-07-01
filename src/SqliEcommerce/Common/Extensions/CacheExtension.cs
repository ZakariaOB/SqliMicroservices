using System.Runtime.Caching;

namespace Common.Extensions
{
    public static class CacheExtension
    {
        private const double DefaultTtl = 24.0;

        /// <summary>
        /// Add to cache with default TTL of 24 hours
        /// </summary>
        /// <param name="cache">The cache instance.</param>
        /// <param name="key">the key with what we will find the object.</param>
        /// <param name="item">the value of the object for the given key.</param>
        public static void AddToCache(
            this ObjectCache cache, 
            string key, 
            object item)
        {
            CacheItemPolicy cacheItemPolicy = new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(DefaultTtl),
            };
            cache.Add(key, item, cacheItemPolicy);
        }

        public static void AddOrUpdateItemInCache(
            this ObjectCache cache, 
            string key, 
            object item)
        {
            if (cache.Contains(key))
            {
                cache.Remove(key);
            }

            cache.AddToCache(key, item);
        }
    }
}
