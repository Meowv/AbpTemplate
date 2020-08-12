using AbpTemplate.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using static AbpTemplate.AbpTemplateCachingConsts;

namespace AbpTemplate
{
    public static class AbpTemplateApplicationCachingExtensions
    {
        public static async Task<TCacheItem> GetOrAddAsync<TCacheItem>(this IDistributedCache cache, string key, Func<Task<TCacheItem>> func, int minutes)
        {
            TCacheItem cacheItem;

            var result = await cache.GetStringAsync(key);

            if (result.IsNullOrEmpty())
            {
                cacheItem = await func.Invoke();

                var options = new DistributedCacheEntryOptions();
                if (minutes != CacheStrategy.NEVER)
                {
                    options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutes);
                }

                await cache.SetStringAsync(key, cacheItem.ToJson(), options);
            }
            else
            {
                cacheItem = result.FromJson<TCacheItem>();
            }

            return cacheItem;
        }
    }
}