using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpTemplateApplicationContractsModule)
    )]
    public class AbpTemplateApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AbpTemplateApplicationModule>(validate: true);
                options.AddProfile<AbpTemplateApplicationAutoMapperProfile>(validate: true);
            });
        }
    }
}