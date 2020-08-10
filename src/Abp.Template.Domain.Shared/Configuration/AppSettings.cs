using Microsoft.Extensions.Configuration;
using System.IO;

namespace Abp.Template.Configuration
{
    public class AppSettings
    {
        private static readonly IConfigurationRoot configuration;

        static AppSettings()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();
        }

        public static string EnabledStorage => configuration["ConnectionStrings:EnabledStorage"];
    }
}