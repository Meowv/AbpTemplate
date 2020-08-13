using AbpTemplate.AppUsers;
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
                b.BsonMap.SetIgnoreExtraElements(true);
            });
        }
    }
}