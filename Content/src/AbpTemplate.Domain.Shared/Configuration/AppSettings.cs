using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AbpTemplate.Configuration
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

        public static class Cors
        {
            static IConfigurationSection Instance => configuration.GetSection("Cors");

            public static string PolicyName => Instance["PolicyName"];

            public static string Origins => Instance["Origins"];
        }

        public static string EnabledStorage => configuration["ConnectionStrings:EnabledStorage"];

        public static string ConnectionString => configuration.GetConnectionString(EnabledStorage);

        public static class Caching
        {
            static IConfigurationSection Instance => configuration.GetSection("Caching");

            public static bool Disabled => Convert.ToBoolean(Instance["Disabled"]);

            public static string RedisConnectionString => Instance["RedisConnectionString"];
        }

        public static class JWT
        {
            static IConfigurationSection Instance => configuration.GetSection("JWT");

            public static int ClockSkew => Convert.ToInt32(Instance["ClockSkew"]);

            public static string ValidAudience => Instance["ValidAudience"];

            public static string ValidIssuer => Instance["ValidIssuer"];

            public static string IssuerSigningKey => Instance["IssuerSigningKey"];

            public static int Expires => Convert.ToInt32(Instance["Expires"]);
        }
    }
}