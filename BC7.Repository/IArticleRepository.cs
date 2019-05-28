﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BC7.Domain;

namespace BC7.Repository
{
    public interface IArticleRepository
    {
        Task<Article> GetAsync(Guid id);
        Task<List<Article>> GetAllAsync();
        Task CreateAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(Guid id);
    }
}