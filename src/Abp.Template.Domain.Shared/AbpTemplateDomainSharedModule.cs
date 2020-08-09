using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.Template
{
    [DependsOn(typeof(AbpLocalizationModule))]
    public class AbpTemplateDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpTemplateDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {

            });
        }
    }
}