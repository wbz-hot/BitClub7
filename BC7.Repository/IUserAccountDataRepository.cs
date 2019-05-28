﻿using System;
using System.Threading.Tasks;
using BC7.Domain;

namespace BC7.Repository
{
    public interface IUserAccountDataRepository
    {
        Task<UserAccountData> GetAsync(Guid id);
        Task<UserAccountData> GetAsync(string emailOrLogin);
        Task UpdateAsync(UserAccountData userAccountData);
    }
}
