using AbpTemplate.AppUsers;
using MongoDB.Bson.Serialization.IdGenerators;
using Volo.Abp;
using Volo.Abp.MongoDB;
using static AbpTemplate.AbpTemplateDbConsts;

namespace AbpTemplate.MongoDb
{
    public static class AbpTemplateDbContextModelCreatingExtensions
    {
        public static void Configure(this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<AppUser>(b =>
            {
                b.CollectionName = TableNames.AppUsers;
                b.BsonMap.AutoMap();
                b.BsonMap.MapIdMember(x => x.Id).SetIdGenerator(ObjectIdGenerator.Instance);
                b.BsonMap.SetIgnoreExtraElements(true);
            });
        }
    }
}