using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Abp.Template.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpTemplateDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCoreSqliteModule),
        typeof(AbpDapperModule)
    )]
    public class AbpTemplateEntityFrameworkCoreModule : AbpModule
    {
    }
}