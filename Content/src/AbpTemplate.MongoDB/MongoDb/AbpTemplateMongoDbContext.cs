using AbpTemplate.AppUsers;
using AbpTemplate.Attribute;
using MongoDB.Driver;
using Volo.Abp.MongoDB;

namespace AbpTemplate.MongoDb
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