using AbpTemplate.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace AbpTemplate.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpTemplateEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpTemplateTestBaseModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCoreSqliteModule)
    )]
    public class AbpTemplateEntityFrameworkCoreTestModule : AbpModule
    {
        private SqliteConnection _sqliteConnection;
        private MySqlConnection _mysqlConnection;
        private SqlConnection _sqlConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            switch (AppSettings.EnabledStorage)
            {
                case "MySql":
                    ConfigureInMySql(context.Services);
                    break;

                case "SqlServer":
                    ConfigureInSqlServer(context.Services);
                    break;

                case "Sqlite":
                    ConfigureInMemorySqlite(context.Services);
                    break;
            }
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            switch (AppSettings.EnabledStorage)
            {
                case "MySql":
                    _mysqlConnection.Dispose();
                    break;

                case "SqlServer":
                    _sqlConnection.Dispose();
                    break;

                case "Sqlite":
                    _sqliteConnection.Dispose();
                    break;
            }
        }

        private void ConfigureInMemorySqlite(IServiceCollection services)
        {
            _sqliteConnection = CreateSqliteDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        private void ConfigureInMySql(IServiceCollection services)
        {
            _mysqlConnection = CreateMySqlDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseMySql(_mysqlConnection);
                });
            });
        }

        private void ConfigureInSqlServer(IServiceCollection services)
        {
            _sqlConnection = CreateSqlServerDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlServer(_sqlConnection);
                });
            });
        }

        private static SqliteConnection CreateSqliteDatabaseAndGetConnection()
        {
            var connection = new SqliteConnection(AppSettings.ConnectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<AbpTemplateMigrationsDbContext>().UseSqlite(connection).Options;

            using (var context = new AbpTemplateMigrationsDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }

        private static MySqlConnection CreateMySqlDatabaseAndGetConnection()
        {
            var connection = new MySqlConnection(AppSettings.ConnectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<AbpTemplateMigrationsDbContext>().UseMySql(connection).Options;

            using (var context = new AbpTemplateMigrationsDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }

        private static SqlConnection CreateSqlServerDatabaseAndGetConnection()
        {
            var connection = new SqlConnection(AppSettings.ConnectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<AbpTemplateMigrationsDbContext>().UseSqlServer(connection).Options;

            using (var context = new AbpTemplateMigrationsDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }
    }
}