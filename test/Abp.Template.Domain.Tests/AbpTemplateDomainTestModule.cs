using Abp.Template.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.Template.Domain.Tests
{
    [DependsOn(typeof(AbpTemplateEntityFrameworkCoreTestModule))]
    public class AbpTemplateDomainTestModule : AbpModule
    {
    }
}