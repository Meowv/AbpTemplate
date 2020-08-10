using Abp.Template.EntityFrameworkCore;
using Abp.Template.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.Template
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTemplateHttpApiModule),
        typeof(AbpTemplateApplicationModule),
        typeof(AbpTemplateApplicationCachingModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpTemplateEntityFrameworkCoreModule),
        typeof(AbpTemplateMongoDbModule)
    )]
    public class AbpTemplateHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            ConfigureConventionalControllers();
            ConfigureRouting(context);
            ConfigureAuthentication(context);
            ConfigureLocalization();
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context, configuration);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AbpTemplateApplicationModule).Assembly);
            });
        }

        private void ConfigureRouting(ServiceConfigurationContext context)
        {
            context.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context)
        {
            context.Services.AddAuthentication();
            context.Services.AddAuthorization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<AbpTemplateDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.Template.Domain.Shared"));
            });
        }

        private void ConfigureRedis(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Caching:RedisConnectionString"];
            });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            var policyName = configuration["Cors:PolicyName"];
            var origins = configuration["Cors:Origins"].Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                       .Select(o => o.RemovePostFix("/"))
                                                       .ToArray();
            context.Services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder =>
                {
                    builder.WithOrigins(origins)
                           .WithAbpExposedHeaders()
                           .SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Abp.Template API",
                    Version = "v1"
                });
                options.DocInclusionPredicate((docName, description) => true);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationId();

            app.UseVirtualFiles();

            app.UseRouting();

            app.UseCors(context.GetConfiguration()["Cors:PolicyName"]);

            app.UseAbpRequestLocalization();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api-docs/{documentName}/swagger.json";
                options.SerializeAsV2 = true;
            });
            app.UseReDoc();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api-docs/v1/swagger.json", "Abp.Template API");
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "Abp.Template API";
            });

            app.UseAuditing();

            app.UseAbpSerilogEnrichers();

            app.UseConfiguredEndpoints();
        }
    }
}