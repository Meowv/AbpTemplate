using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Abp.Template.EntityFrameworkCore
{
    public static class AbpTemplateDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
        }
    }
}