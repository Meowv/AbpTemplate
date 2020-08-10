using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.Template.EntityFrameworkCore
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