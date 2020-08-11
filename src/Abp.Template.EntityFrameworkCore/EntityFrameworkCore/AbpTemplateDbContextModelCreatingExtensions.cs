using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.Template.EntityFrameworkCore
{
    public static class AbpTemplateDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<AppUser>(b =>
            {
                b.ToTable("app_users");

                b.ConfigureByConvention();
            });
        }
    }
}