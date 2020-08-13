using AbpTemplate.AppUsers;
using AbpTemplate.Attribute;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpTemplate.EntityFrameworkCore
{
    [ConnectionString]
    public class AbpTemplateDbContext : AbpDbContext<AbpTemplateDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        public AbpTemplateDbContext(DbContextOptions<AbpTemplateDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configure();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}