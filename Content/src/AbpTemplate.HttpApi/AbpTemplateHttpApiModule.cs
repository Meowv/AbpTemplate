using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpTemplateApplicationContractsModule)
    )]
    public class AbpTemplateHttpApiModule : AbpModule
    {
    }
}