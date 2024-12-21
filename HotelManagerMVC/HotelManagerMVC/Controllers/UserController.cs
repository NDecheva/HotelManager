using AutoMapper;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Enum;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using HotelManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]

    public class UserController : BaseCrudController<UserDto, IUserRepository, IUserService, UserEditVM, UserDetailsVM>

    {
        

        public UserController(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }


        protected override async Task<UserEditVM> PrePopulateVMAsync(UserEditVM editVM)
        {

            editVM.Roles = Enum.GetValues(typeof(Role)).Cast<Role>().Select(s => new SelectListItem(s.ToString(), ((int)s).ToString())).ToList();

            return editVM;
        }
    }
}
