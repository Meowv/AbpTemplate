using Abp.Template.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.Template.Controller
{
    public abstract class AbpTemplateController : AbpController
    {
        protected AbpTemplateController()
        {
            LocalizationResource = typeof(AbpTemplateResource);
        }
    }
}