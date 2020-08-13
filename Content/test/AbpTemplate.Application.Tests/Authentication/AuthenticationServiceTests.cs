using AbpTemplate.Extensions;
using AbpTemplate.Response;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AbpTemplate.Authentication
{
    public class AuthenticationServiceTests : AbpTemplateApplicationTestBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _authenticationService = GetRequiredService<IAuthenticationService>();
        }

        [Fact]
        public async Task Generate_Token_Test()
        {
            var serviceResult = await _authenticationService.GenerateTokenAsync();

            serviceResult.Success.ShouldBeTrue();
            serviceResult.Result.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task Unauthorized_Test()
        {
            using var _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44312")
            };

            var response = await _httpClient.GetAsync($"/api/authentication/test");

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Authorized_Test()
        {
            var serviceResult = await _authenticationService.GenerateTokenAsync();

            serviceResult.Success.ShouldBeTrue();
            serviceResult.Result.ShouldNotBeEmpty();

            using var _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44312")
            };

            var response = await _httpClient.GetAsync($"/api/authentication/test?token={serviceResult.Result}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();

            result.FromJson<ServiceResult>().Success.ShouldBeTrue();
        }
    }
}