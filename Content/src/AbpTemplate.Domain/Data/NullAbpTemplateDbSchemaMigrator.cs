using System.Threading.Tasks;

namespace AbpTemplate.Data
{
    public class NullAbpTemplateDbSchemaMigrator : IAbpTemplateDbSchemaMigrator
    {
        public Task MigrateAsync() => Task.CompletedTask;
    }
}