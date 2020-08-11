using AbpTemplate.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpTemplate.Domain.Tests
{
    [DependsOn(typeof(AbpTemplateEntityFrameworkCoreTestModule))]
    public class AbpTemplateDomainTestModule : AbpModule
    {
    }
}