using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace AbpTemplate
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
               .WriteTo.Async(c => c.File($"Logs/{DateTime.Now:yyyy/MMdd}/logs.txt"))
               .CreateLogger();

            try
            {
                Log.Information("Starting AbpTemplate.HttpApi.Host.");

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
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    LogManager.UseConsoleLogging(LogLevel.Trace);

                    builder.AddApollo(builder.Build().GetSection("Apollo")).AddDefault();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration()
                              .UseStartup<Startup>();
                }).UseAutofac().UseSerilog();
    }
}