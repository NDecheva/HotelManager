using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Shared.Services
{
    public interface IUserService : IBaseCrudService<UserDto, IUserRepository>
    {

    }
}

