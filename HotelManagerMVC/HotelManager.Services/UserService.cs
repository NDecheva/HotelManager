using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Security;
using HotelManager.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Services
{
    [AutoBind]
    public class UserService : BaseCrudService<UserDto, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {

        }

        public async Task<bool> CanUserLoginAsync(string username, string password)
        {
            var user = await _repository.GetByUsernameAsync(username);

            if (user == null)
            {
                return false;
            }

            bool passwordMatches = PasswordHasher.VerifyPassword(password, user.Password);

            return passwordMatches;
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            return await _repository.GetByUsernameAsync(username);
        }
    }
}
