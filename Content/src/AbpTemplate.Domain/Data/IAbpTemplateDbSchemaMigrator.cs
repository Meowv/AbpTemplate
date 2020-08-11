using System.Threading.Tasks;

namespace AbpTemplate.Data
{
    public interface IAbpTemplateDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}