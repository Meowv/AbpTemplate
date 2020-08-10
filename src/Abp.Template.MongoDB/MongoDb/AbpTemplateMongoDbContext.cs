using Abp.Template.Attribute;
using Volo.Abp.MongoDB;

namespace Abp.Template.MongoDb
{
    [ConnectionString]
    public class AbpTemplateMongoDbContext : AbpMongoDbContext
    {
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Configure();
        }
    }
}