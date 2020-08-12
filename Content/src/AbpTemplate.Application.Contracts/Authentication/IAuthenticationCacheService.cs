using AbpTemplate.Response;
using System;
using System.Threading.Tasks;

namespace AbpTemplate.Authentication
{
    public interface IAuthenticationCacheService
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync(Func<Task<ServiceResult<string>>> func);
    }
}