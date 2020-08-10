using Abp.Template.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Abp.Template
{
    [DependsOn(typeof(AbpValidationModule))]
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
                options.Resources
                       .Add<AbpTemplateResource>("en")
                       .AddBaseTypes(typeof(AbpValidationResource))
                       .AddVirtualJson("/Localization/AbpTemplate");

                options.DefaultResourceType = typeof(AbpTemplateResource);
            });
        }
    }
}