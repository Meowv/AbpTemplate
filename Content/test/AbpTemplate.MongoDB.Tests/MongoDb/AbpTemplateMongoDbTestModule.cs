using AbpTemplate.MongoDb;
using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace AbpTemplate.MongoDB
{
    [DependsOn(
        typeof(AbpTemplateTestBaseModule),
        typeof(AbpTemplateMongoDbModule)
    )]
    public class AbpTemplateMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = AbpTemplateMongoDbFixture.ConnectionString.EnsureEndsWith('/') + "Db_" + Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}