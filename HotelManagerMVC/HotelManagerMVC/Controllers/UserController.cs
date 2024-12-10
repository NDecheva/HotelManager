using AutoMapper;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Enum;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using HotelManagerMVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagerMVC.Controllers
{
    public class UserController : BaseCrudController<UserDto, IUserRepository, IUserService, UserEditVM, UserDetailsVM>

    {
        

        public UserController(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }


        protected override async Task<UserEditVM> PrePopulateVMAsync(UserEditVM editVM)
        {

            editVM.Roles = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(s => new SelectListItem(s.ToString(), ((int)s).ToString()));

            return editVM;
        }
    }
}
