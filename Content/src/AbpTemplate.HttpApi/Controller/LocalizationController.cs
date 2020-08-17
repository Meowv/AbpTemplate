using AbpTemplate.Localization;
using Exceptionless;
using Exceptionless.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

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
        public Dictionary<string, string> Get()
        {
            ExceptionlessClient.Default.CreateLog("LocalizationController", "Getting results", LogLevel.Info).Submit();
            throw new Exception($"Random AspNetCore Exception: {Guid.NewGuid()}");
        }
    }
}