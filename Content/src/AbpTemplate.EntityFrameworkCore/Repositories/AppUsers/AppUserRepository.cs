using AbpTemplate.AppUsers;
using AbpTemplate.AppUsers.Repositories;
using AbpTemplate.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbpTemplate.Repositories.AppUsers
{
    public class AppUserRepository : EfCoreRepository<AbpTemplateDbContext, AppUser, Guid>, IAppUserRepository
    {
        public AppUserRepository(IDbContextProvider<AbpTemplateDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}