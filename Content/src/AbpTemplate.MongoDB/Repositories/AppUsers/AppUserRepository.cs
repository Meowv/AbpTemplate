using AbpTemplate.AppUsers;
using AbpTemplate.AppUsers.Repositories;
using AbpTemplate.MongoDb;
using System;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace AbpTemplate.Repositories.AppUsers
{
    public class AppUserRepository : MongoDbRepository<AbpTemplateMongoDbContext, AppUser, Guid>, IAppUserRepository
    {
        public AppUserRepository(IMongoDbContextProvider<AbpTemplateMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}