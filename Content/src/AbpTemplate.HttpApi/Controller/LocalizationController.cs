using AbpTemplate.Localization;
using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;

namespace AbpTemplate.Controller
{
    [Route("api/[controller]")]
    public class LocalizationController : AbpTemplateController
    {
        private readonly IStringLocalizer<AbpTemplateResource> _localizer;


        public LocalizationController(IStringLocalizer<AbpTemplateResource> localizer)
        {
            _localizer = localizer;
        }

        /// <summary>
        /// HelloWorld
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("HelloWorld")]
        public string HelloWorld()
        {
            return _localizer["HelloWorld"];
        }

        /// <summary>
        /// Exception
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Exception")]
        public string ExceptionlessTest()
        {
            ExceptionlessClient.Default.CreateLog("LocalizationController", "Getting results", LogLevel.Info).Submit();
            throw new Exception($"Random AspNetCore Exception: {Guid.NewGuid()}");
        }

        /// <summary>
        /// Apollo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Apollo")]
        public IActionResult ApolloTest([FromServices] IConfiguration configuration, string key)
        {
            return Content(configuration.GetValue<string>(key));
        }
    }
}