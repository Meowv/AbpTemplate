using System.Threading.Tasks;

namespace Abp.Template.Data
{
    public interface IAbpTemplateDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}