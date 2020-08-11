using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpTemplate.EntityFrameworkCore
{
    [DependsOn(typeof(AbpTemplateEntityFrameworkCoreModule))]
    public class AbpTemplateEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AbpTemplateMigrationsDbContext>();
        }
    }
}