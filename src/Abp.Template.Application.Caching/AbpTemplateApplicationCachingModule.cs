using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace Abp.Template
{
    [DependsOn(
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpTemplateApplicationContractsModule)
    )]
    public class AbpTemplateApplicationCachingModule : AbpModule
    {
    }
}