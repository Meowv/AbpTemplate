using Abp.Template.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.Template.EntityFrameworkCore
{
    public class EntityFrameworkCoreAbpTemplateDbSchemaMigrator : IAbpTemplateDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreAbpTemplateDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            await _serviceProvider.GetRequiredService<AbpTemplateMigrationsDbContext>().Database.MigrateAsync();
        }
    }
}