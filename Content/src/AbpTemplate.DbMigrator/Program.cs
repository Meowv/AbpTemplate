using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Threading.Tasks;

namespace AbpTemplate.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
#if DEBUG
               .MinimumLevel.Override("AbpTemplate", LogEventLevel.Debug)
#else
               .MinimumLevel.Override("AbpTemplate", LogEventLevel.Information)
#endif
               .Enrich.FromLogContext()
               .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs/logs.txt"))
               .WriteTo.Console()
               .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((context, logging) => logging.ClearProviders())
                 .ConfigureServices((hostContext, services) =>
                 {
                     services.AddHostedService<DbMigratorHostedService>();
                 });
    }
}