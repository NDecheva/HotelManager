using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManager.Shared.Repos.Contracts;

public abstract class BaseRepository<T, TModel> : IBaseRepository<TModel>,IDisposable
	where T : class, IBaseEntity
	where TModel : BaseModel
{
	protected readonly DbContext _context;
	protected readonly DbSet<T> _dbSet;
	protected readonly IMapper mapper;
	private bool disposedValue;

	protected BaseRepository(HotelManagerDbContext context,IMapper mapper)
    {
		_context = context;
		_dbSet = _context.Set<T>();
		this.mapper = mapper;
	}

	public virtual TModel MapToModel(T entity)
    {
		return mapper.Map<TModel>(entity);
	}
	public virtual T MapToEntity(TModel model)
	{
		return mapper.Map<T>(model);
	}
	public virtual IEnumerable<TModel> MapToEnumerableOfModel(IEnumerable<T> entities)
	{
		return mapper.Map<IEnumerable<TModel>>(entities);
	}

	public async Task<IEnumerable<TModel>> GetAllAsync()
    {
		return this.MapToEnumerableOfModel(await _dbSet.ToListAsync());
    }

	public async Task<TModel> GetByIdAsync(int id)
    {
		var user = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
			throw new ArgumentNullException(nameof(user));
        }
		return this.MapToModel(user);
    }

	public async Task CreateAsync(TModel model)
    {
        if (model == null)
        {
			throw new ArgumentNullException(nameof(model));
        }

        try
        {
			var entity = this.MapToEntity(model);
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
        }
        catch (SqlException ex)
        {
			//Here we can save these errors in some logs or telemetery
			await Console.Out.WriteLineAsync($"The system threw an sql exception trying to create {nameof(model)}: {ex.Message}");
        }
		catch (Exception ex)
		{
			//Here we can save these errors in some logs or telemetery
			await Console.Out.WriteLineAsync($"The system threw a non-sql exception trying to create {nameof(model)}: {ex.Message}");
		}
	}

	public async Task UpdateAsync(TModel model)
	{
		if (model == null)
		{
			throw new ArgumentNullException(nameof(model));
		}

		try
		{
			var entity = await this._dbSet.FindAsync(model.Id);
            if (entity == null)
            {
				throw new ArgumentNullException(nameof(entity));
			}
			_context.Entry(entity).CurrentValues.SetValues(model);
			await _context.SaveChangesAsync();
		}
		catch (SqlException ex)
		{
			//Here we can save these errors in some logs or telemetery
			await Console.Out.WriteLineAsync($"The system threw an sql exception trying to create {nameof(model)}: {ex.Message}");
		}
		catch (Exception ex)
		{
			//Here we can save these errors in some logs or telemetery
			await Console.Out.WriteLineAsync($"The system threw a non-sql exception trying to create {nameof(model)}: {ex.Message}");
		}
	}


	public async Task SaveAsync(TModel model)
    {
        if (model == null)
        {
			throw new ArgumentNullException(nameof(model));
		}
		if (model.Id != 0)
        {
            await UpdateAsync(model);
        }
		else
			await CreateAsync(model);
    }

	public async Task DeleteAsync(int id)
	{
		var entity = await this._dbSet.FindAsync(id);

		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		try
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}
		catch (SqlException ex)
		{
			//Here we can save these errors in some logs or telemetery
			await Console.Out.WriteLineAsync($"The system threw an sql exception trying to create {nameof(entity)}: {ex.Message}");
		}
		catch (Exception ex)
		{
			//Here we can save these errors in some logs or telemetery
			await Console.Out.WriteLineAsync($"The system threw a non-sql exception trying to create {nameof(entity)}: {ex.Message}");
		}
	}

	public Task<bool> ExistsByIdAsync(int id)
    {
		return _dbSet.AnyAsync(e => e.Id == id);
    }

	public async Task<IEnumerable<TModel>> GetWithPaginationAsync(int pageSize,int pageNumber)
    {
		var paginatedRecords = await _dbSet
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return MapToEnumerableOfModel(paginatedRecords);
    }

	protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
				_context.Dispose();
            }
			disposedValue = true;
        }
    }


	public void Dispose()
    {
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
    }
}
