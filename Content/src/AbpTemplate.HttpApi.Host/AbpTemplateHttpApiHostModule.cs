using AbpTemplate.Configuration;
using AbpTemplate.EntityFrameworkCore;
using AbpTemplate.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

namespace AbpTemplate
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
            ConfigureConventionalControllers();
            ConfigureRouting(context);
            ConfigureAuthentication(context);
            ConfigureLocalization();
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context);
            ConfigureCors(context);
            ConfigureSwaggerServices(context);
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers
                       .Create(typeof(AbpTemplateApplicationModule).Assembly, opts =>
                       {
                           opts.RootPath = "AbpTemplate";
                       });
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
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.FromSeconds(AppSettings.JWT.ClockSkew),
                           ValidateIssuerSigningKey = true,
                           ValidAudience = AppSettings.JWT.ValidAudience,
                           ValidIssuer = AppSettings.JWT.ValidIssuer,
                           IssuerSigningKey = new SymmetricSecurityKey(AppSettings.JWT.IssuerSigningKey.GetBytes())
                       };

                       options.Events = new JwtBearerEvents
                       {

                       };
                   });

            context.Services.AddAuthorization();

            context.Services.AddHttpClient();
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
                options.FileSets.ReplaceEmbeddedByPhysical<AbpTemplateDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}AbpTemplate.Domain.Shared"));
            });
        }

        private void ConfigureRedis(ServiceConfigurationContext context)
        {
            if (!AppSettings.Caching.Disabled)
            {
                context.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppSettings.Caching.RedisConnectionString;
                });
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(AppSettings.Cors.PolicyName, builder =>
                {
                    builder.WithOrigins(AppSettings.Cors
                                                   .Origins
                                                   .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(o => o.RemovePostFix("/"))
                                                   .ToArray())
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
                    Title = "AbpTemplate API",
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

            app.UseHsts();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCorrelationId();

            app.UseVirtualFiles();

            app.UseRouting();

            app.UseCors(AppSettings.Cors.PolicyName);

            app.UseAbpRequestLocalization();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api-docs/{documentName}/swagger.json";
                options.SerializeAsV2 = true;
            });
            app.UseReDoc();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api-docs/v1/swagger.json", "AbpTemplate API");
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "AbpTemplate API";
            });

            app.UseAuditing();

            app.UseAbpSerilogEnrichers();

            app.UseConfiguredEndpoints();
        }
    }
}