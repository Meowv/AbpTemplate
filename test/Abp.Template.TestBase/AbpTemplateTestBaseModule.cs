using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Abp.Template
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