using AbpTemplate.Domain.Tests;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpTemplateApplicationModule),
        typeof(AbpTemplateDomainTestModule)
    )]
    public class AbpTemplateApplicationTestModule : AbpModule
    {
    }
}