using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpTemplateDomainSharedModule)
    )]
    public class AbpTemplateDomainModule : AbpModule
    {
    }
}