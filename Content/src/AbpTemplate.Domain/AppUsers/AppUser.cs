using System;
using Volo.Abp.Domain.Entities;

namespace AbpTemplate.AppUsers
{
    public class AppUser : Entity<Guid>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}