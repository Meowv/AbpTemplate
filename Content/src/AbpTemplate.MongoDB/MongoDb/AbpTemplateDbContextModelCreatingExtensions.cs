using Volo.Abp;
using Volo.Abp.MongoDB;

namespace AbpTemplate.MongoDb
{
    public static class AbpTemplateDbContextModelCreatingExtensions
    {
        public static void Configure(this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<AppUser>(b =>
            {
                b.CollectionName = "app_users";
            });
        }
    }
}