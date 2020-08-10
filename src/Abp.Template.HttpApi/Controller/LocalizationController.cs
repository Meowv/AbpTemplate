using Abp.Template.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Abp.Template.Controller
{
    [Route("api/[controller]")]
    public class LocalizationController : AbpTemplateController
    {
        private readonly IStringLocalizer<AbpTemplateResource> _localizer;

        public LocalizationController(IStringLocalizer<AbpTemplateResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        [Route("HelloWorld")]
        public string HelloWorld()
        {
            return _localizer["HelloWorld"];
        }
    }
}