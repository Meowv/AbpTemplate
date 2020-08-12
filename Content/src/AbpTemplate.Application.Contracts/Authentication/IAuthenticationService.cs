using AbpTemplate.Response;
using System.Threading.Tasks;

namespace AbpTemplate.Authentication
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> GenerateTokenAsync();
    }
}