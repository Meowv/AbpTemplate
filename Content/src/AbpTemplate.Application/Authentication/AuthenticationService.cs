using AbpTemplate.Configuration;
using AbpTemplate.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AbpTemplate.Authentication
{
    [Route("Api/Authentication")]
    public class AuthenticationService : ApplicationServiceBase, IAuthenticationService
    {
        private readonly IAuthenticationCacheService _authenticationCacheService;

        public AuthenticationService(IAuthenticationCacheService authenticationCacheService)
        {
            _authenticationCacheService = authenticationCacheService;
        }

        [HttpGet]
        [Route("Token")]
        public async Task<ServiceResult<string>> GenerateTokenAsync()
        {
            return await _authenticationCacheService.GenerateTokenAsync(async () =>
            {
                var result = new ServiceResult<string>();

                var claims = new[] {
                    new Claim(ClaimTypes.Name, "阿星Plus"),
                    new Claim(ClaimTypes.Email, "123@meowv.com"),
                    new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(AppSettings.JWT.Expires)).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
                };

                var key = new SymmetricSecurityKey(AppSettings.JWT.IssuerSigningKey.GetBytes());
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    issuer: AppSettings.JWT.ValidIssuer,
                    audience: AppSettings.JWT.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(AppSettings.JWT.Expires),
                    signingCredentials: creds);

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                result.IsSuccess(token);
                return await Task.FromResult(result);
            });
        }

        [HttpGet]
        [Authorize]
        [Route("Test")]
        public async Task<ServiceResult> Test()
        {
            return await Task.FromResult(new ServiceResult());
        }
    }
}