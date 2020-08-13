using System;
using Volo.Abp.Domain.Repositories;

namespace AbpTemplate.AppUsers.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser, Guid>
    {
    }
}