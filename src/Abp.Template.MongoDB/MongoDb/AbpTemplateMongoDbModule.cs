using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Abp.Template.MongoDb
{
    [DependsOn(
        typeof(AbpMongoDbModule),
        typeof(AbpTemplateDomainModule)
    )]
    public class AbpTemplateMongoDbModule : AbpModule
    {
    }
}