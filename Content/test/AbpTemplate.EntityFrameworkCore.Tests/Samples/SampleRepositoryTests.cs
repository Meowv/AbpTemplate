using AbpTemplate.AppUsers;
using AbpTemplate.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace AbpTemplate.Samples
{
    public class SampleRepositoryTests : AbpTemplateEntityFrameworkCoreTestBase
    {
        private readonly IRepository<AppUser, Guid> _appUserRepository;

        public SampleRepositoryTests()
        {
            _appUserRepository = GetRequiredService<IRepository<AppUser, Guid>>();
        }

        [Fact]
        public async Task Should_Query_AppUser()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                var adminUser = await _appUserRepository.Where(u => u.UserName == "admin").FirstOrDefaultAsync();

                adminUser.ShouldBeNull();
            });
        }
    }
}