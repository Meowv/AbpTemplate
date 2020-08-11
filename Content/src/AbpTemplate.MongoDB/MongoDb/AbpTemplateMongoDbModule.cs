using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace AbpTemplate.MongoDb
{
    [DependsOn(
        typeof(AbpMongoDbModule),
        typeof(AbpTemplateDomainModule)
    )]
    public class AbpTemplateMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<AbpTemplateMongoDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}