using AbpTemplate.Response;
using System;
using System.Threading.Tasks;

namespace AbpTemplate.Authentication
{
    public interface IAuthenticationCacheService
    {
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(Func<Task<ServiceResult<string>>> func);
    }
}