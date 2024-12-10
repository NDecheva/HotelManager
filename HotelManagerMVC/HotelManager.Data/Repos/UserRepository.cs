using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManager.Shared.Repos.Contracts;

[AutoBind]
public class UserRepository : BaseRepository<User, UserDto>, IUserRepository
{
	public UserRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
	{

	}
    public async Task<UserDto> GetByUsernameAsync(string username)
    {
        return MapToModel(await _dbSet.FirstOrDefaultAsync(u => u.Username == username));
    }
}
