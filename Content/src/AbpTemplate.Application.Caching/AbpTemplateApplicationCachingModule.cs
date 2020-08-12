using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
        typeof(AbpTemplateApplicationContractsModule)
    )]
    public class AbpTemplateApplicationCachingModule : AbpModule
    {
    }
}