using Abp.Template.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Abp.Template.DbMigrator
{
    [DependsOn(
       typeof(AbpAutofacModule),
       typeof(AbpTemplateEntityFrameworkCoreModule)
       )]
    public class AbpTemplateDbMigratorModule : AbpModule
    {
    }
}