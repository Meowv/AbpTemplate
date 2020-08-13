using AbpTemplate.EntityFrameworkCore;
using AbpTemplate.MongoDB;
using Volo.Abp.Modularity;

namespace AbpTemplate.Domain.Tests
{
    [DependsOn(
        typeof(AbpTemplateEntityFrameworkCoreTestModule),
        typeof(AbpTemplateMongoDbTestModule)
    )]
    public class AbpTemplateDomainTestModule : AbpModule
    {
    }
}