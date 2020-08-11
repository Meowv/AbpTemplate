using AbpTemplate.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpTemplate.Controller
{
    public abstract class AbpTemplateController : AbpController
    {
        protected AbpTemplateController()
        {
            LocalizationResource = typeof(AbpTemplateResource);
        }
    }
}