using AbpTemplate.AppUsers;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using static AbpTemplate.AbpTemplateDbConsts;

namespace AbpTemplate.EntityFrameworkCore
{
    public static class AbpTemplateDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(TableNames.AppUsers);
                b.HasKey(x => x.Id);
                b.Property(x => x.UserName).HasMaxLength(20).IsRequired();
                b.Property(x => x.Password).HasMaxLength(20).IsRequired();
                b.Property(x => x.Email).HasMaxLength(50).IsRequired();

                b.ConfigureByConvention();
            });
        }
    }
}