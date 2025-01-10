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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin, User")]

    public class RoomController : BaseCrudController<RoomDto, IRoomRepository, IRoomService, RoomEditVM, RoomDetailsVM>
    {
        public RoomController(IRoomService service, IMapper mapper) : base(service, mapper)
        {
        }

        protected override async Task<RoomEditVM> PrePopulateVMAsync(RoomEditVM editVM)
        {
            editVM.RoomTypeList = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(s => new SelectListItem(s.ToString(), ((int)s).ToString())).ToList();
            return editVM;


        }

    }
}
