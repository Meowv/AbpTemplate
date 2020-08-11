using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpTemplate.EntityFrameworkCore
{
    public class AbpTemplateMigrationsDbContext : AbpDbContext<AbpTemplateMigrationsDbContext>
    {
        public AbpTemplateMigrationsDbContext(DbContextOptions<AbpTemplateMigrationsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configure();
        }
    }
}