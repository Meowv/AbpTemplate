using Abp.Template.Attribute;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.Template.EntityFrameworkCore
{
    [ConnectionString]
    public class AbpTemplateDbContext : AbpDbContext<AbpTemplateDbContext>
    {
        public AbpTemplateDbContext(DbContextOptions<AbpTemplateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configure();
        }
    }
}