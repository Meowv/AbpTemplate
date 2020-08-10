using System.Threading.Tasks;

namespace Abp.Template.Data
{
    public class NullAbpTemplateDbSchemaMigrator : IAbpTemplateDbSchemaMigrator
    {
        public Task MigrateAsync() => Task.CompletedTask;
    }
}