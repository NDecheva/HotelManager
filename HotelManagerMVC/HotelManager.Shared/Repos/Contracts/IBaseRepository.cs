﻿using HotelManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Shared.Repos.Contracts
{
    public interface IBaseRepository<TModel>
        where TModel : BaseModel
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);
        Task CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task SaveAsync(TModel model);
        Task DeleteAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        Task<IEnumerable<TModel>> GetWithPaginationAsync(int pageSize, int pageNumber);


    }
}