using AbpTemplate.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpTemplate.DbMigrator
{
    [DependsOn(
       typeof(AbpAutofacModule),
       typeof(AbpTemplateEntityFrameworkCoreModule)
       )]
    public class AbpTemplateDbMigratorModule : AbpModule
    {
    }
}