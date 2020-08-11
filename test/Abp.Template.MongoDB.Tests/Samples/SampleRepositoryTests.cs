using Abp.Template.MongoDb;
using MongoDB.Driver.Linq;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Abp.Template.Samples
{
    [Collection(AbpTemplateTestConsts.CollectionDefinitionName)]
    public class SampleRepositoryTests : AbpTemplateMongoDbTestBase
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
                var adminUser = await _appUserRepository.GetMongoQueryable().FirstOrDefaultAsync(u => u.UserName == "admin");

                adminUser.ShouldBeNull();
            });
        }
    }
}