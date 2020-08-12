using AbpTemplate.Response;
using System;
using System.Threading.Tasks;
using static AbpTemplate.AbpTemplateCachingConsts;

namespace AbpTemplate.Authentication
{
    public class AuthenticationCacheService : ApplicationCachingServiceBase, IAuthenticationCacheService
    {
        public const string KEY_TOKEN = "Authentication:Token";

        public async Task<ServiceResult<string>> GenerateTokenAsync(Func<Task<ServiceResult<string>>> func)
        {
            return await Cache.GetOrAddAsync(KEY_TOKEN, func, CacheStrategy.FIVE_MINUTES);
        }
    }
}