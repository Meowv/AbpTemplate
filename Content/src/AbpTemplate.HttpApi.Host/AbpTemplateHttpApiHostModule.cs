﻿using AbpTemplate.Configuration;
using AbpTemplate.EntityFrameworkCore;
using AbpTemplate.Extensions;
using AbpTemplate.Filters;
using AbpTemplate.MongoDb;
using AbpTemplate.Response;
using Exceptionless;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
                           OnChallenge = async context =>
                           {
                               context.HandleResponse();

                               context.Response.ContentType = "application/json;charset=utf-8";
                               context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                               var result = new ServiceResult();
                               result.IsFailed(nameof(HttpStatusCode.Unauthorized));

                               await context.Response.WriteAsync(result.ToJson());
                           },
                           OnMessageReceived = async context =>
                           {
                               context.Token = context.Request.Query["token"];

                               await Task.CompletedTask;
                           }
                       };
                   });

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
                    Description = @"<a class=""link"" target=""_blank"" href=""https://meowv.com"">https://meowv.com</a>、<a class=""link"" target=""_blank"" href=""https://github.com/meowv"">https://github.com/meowv</a>、<a class=""link"" target=""_blank"" href=""https://www.nuget.org/profiles/qix"">https://www.nuget.org/profiles/qix</a>",
                    Version = "v1.0.0"
                });

                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "AbpTemplate.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "AbpTemplate.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "AbpTemplate.Application.Contracts.xml"));

                var security = new OpenApiSecurityScheme
                {
                    Description = "<b>please enter <code>Bearer {Token}</code> for authentication.</b>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                options.AddSecurityDefinition("oauth2", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, null } });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.DocumentFilter<SwaggerDocumentFilter>();
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

            app.UseExceptionless(AppSettings.Exceptionless.ApiKey);

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
            app.UseReDoc(options =>
            {
                options.DocumentTitle = "⚡AbpTemplate API Docs";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api-docs/v1/swagger.json", "AbpTemplate API");
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.List);
                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "⚡AbpTemplate API";
            });

            app.UseAuditing();

            app.UseAbpSerilogEnrichers();

            app.UseConfiguredEndpoints();
        }
    }
}