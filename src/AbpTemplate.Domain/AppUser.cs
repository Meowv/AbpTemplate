﻿using System;
using Volo.Abp.Domain.Entities;

namespace AbpTemplate
{
    public class AppUser : Entity<Guid>
    {
        public virtual string UserName { get; private set; }

        public virtual string Email { get; private set; }
    }
}