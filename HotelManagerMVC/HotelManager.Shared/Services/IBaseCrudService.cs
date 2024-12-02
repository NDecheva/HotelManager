using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Shared.Services
{
    public interface IBaseCrudService<TModel, TRepository>
        where TModel : BaseModel
        where TRepository : IBaseRepository<TModel>
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdIfExistsAsync(int id);
        Task SaveAsync(TModel model);
        Task DeleteAsync(int id);
        Task<IEnumerable<TModel>> GetWithPaginationAsync(int pageSize, int pageNumber);
        Task<bool> ExistsByIdAsync(int id);

    }
}