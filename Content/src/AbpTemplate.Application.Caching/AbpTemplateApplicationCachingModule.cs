using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpTemplateApplicationContractsModule)
    )]
    public class AbpTemplateApplicationCachingModule : AbpModule
    {
    }
}