using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Abp.Template
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpTemplateApplicationContractsModule)
    )]
    public class AbpTemplateHttpApiModule : AbpModule
    {
    }
}