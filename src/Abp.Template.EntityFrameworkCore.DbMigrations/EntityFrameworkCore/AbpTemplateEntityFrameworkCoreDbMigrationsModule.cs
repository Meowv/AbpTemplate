using Volo.Abp.Modularity;

namespace Abp.Template.EntityFrameworkCore
{
    [DependsOn(typeof(AbpTemplateEntityFrameworkCoreModule))]
    public class AbpTemplateEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
    }
}