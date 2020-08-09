using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Abp.Template
{
    [DependsOn(typeof(AbpDddApplicationContractsModule))]
    public class AbpTemplateApplicationContractsModule : AbpModule
    {
    }
}