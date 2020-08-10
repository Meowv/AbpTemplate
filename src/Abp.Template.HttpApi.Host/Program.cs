using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace Abp.Template
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
               .MinimumLevel.Debug()
#else
               .MinimumLevel.Information()
#endif
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .Enrich.FromLogContext()
               .WriteTo.Async(c => c.File("Logs/logs.txt"))
               .CreateLogger();

            try
            {
                Log.Information("Starting Abp.Template.HttpApi.Host.");

                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration()
                              .UseStartup<Startup>();
                              //.UseUrls($"http://*:{5000}");
                }).UseAutofac().UseSerilog();
    }
}