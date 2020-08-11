using Abp.Template.Attribute;
using MongoDB.Driver;
using Volo.Abp.MongoDB;

namespace Abp.Template.MongoDb
{
    [ConnectionString]
    public class AbpTemplateMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<AppUser> Users => Collection<AppUser>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Configure();
        }
    }
}