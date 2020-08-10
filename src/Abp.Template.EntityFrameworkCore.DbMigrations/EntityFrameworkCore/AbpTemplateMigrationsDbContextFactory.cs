using Abp.Template.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Abp.Template.EntityFrameworkCore
{
    public class AbpTemplateMigrationsDbContextFactory : IDesignTimeDbContextFactory<AbpTemplateMigrationsDbContext>
    {
        public AbpTemplateMigrationsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AbpTemplateMigrationsDbContext>();

            switch (AppSettings.EnabledStorage)
            {
                case "MySql":
                    builder.UseMySql(AppSettings.ConnectionString);
                    break;

                case "SqlServer":
                    builder.UseSqlServer(AppSettings.ConnectionString);
                    break;

                case "Sqlite":
                    builder.UseSqlite(AppSettings.ConnectionString);
                    break;
            }

            return new AbpTemplateMigrationsDbContext(builder.Options);
        }
    }
}