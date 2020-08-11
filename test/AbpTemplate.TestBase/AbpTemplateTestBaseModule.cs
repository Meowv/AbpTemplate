using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpTemplate
{
    [DependsOn(
       typeof(AbpAutofacModule),
       typeof(AbpTestBaseModule),
       typeof(AbpTemplateDomainModule)
    )]
    public class AbpTemplateTestBaseModule : AbpModule
    {
    }
}