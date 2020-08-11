using Abp.Template.Domain.Tests;
using Volo.Abp.Modularity;

namespace Abp.Template
{
    [DependsOn(
        typeof(AbpTemplateApplicationModule),
        typeof(AbpTemplateDomainTestModule)
    )]
    public class AbpTemplateApplicationTestModule: AbpModule
    {
    }
}