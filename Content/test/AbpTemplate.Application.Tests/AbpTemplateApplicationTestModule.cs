using AbpTemplate.Configuration;
using AbpTemplate.Domain.Tests;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpTemplateApplicationModule),
        typeof(AbpTemplateApplicationCachingModule),
        typeof(AbpTemplateDomainTestModule)
    )]
    public class AbpTemplateApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            if (!AppSettings.Caching.Disabled)
            {
                context.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppSettings.Caching.RedisConnectionString;
                });
            }
        }
    }
}