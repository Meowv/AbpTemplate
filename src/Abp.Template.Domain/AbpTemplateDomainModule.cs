using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Abp.Template
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpTemplateDomainSharedModule)
    )]
    public class AbpTemplateDomainModule : AbpModule
    {
    }
}