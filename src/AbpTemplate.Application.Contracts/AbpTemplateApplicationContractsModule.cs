using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpTemplateDomainSharedModule),
        typeof(AbpDddApplicationContractsModule)
    )]
    public class AbpTemplateApplicationContractsModule : AbpModule
    {
    }
}