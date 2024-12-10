using AutoMapper;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using HotelManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin, User")]
    public class ReservationController :BaseCrudController<ReservationDto, IReservationRepository, IReservationService, ReservationEditVM, ReservationDetailsVM>
    {
        public IUserService _userService { get; set; }
        public IRoomService _roomService { get; set; }

        public ReservationController(IReservationService _service, IUserService _userService, IRoomService _roomService, IMapper mapper):base(_service,mapper)
        {
            this._userService = _userService;
            this._roomService = _roomService;
        }
        protected virtual async Task<ReservationEditVM> PrePopulateVMAsync(ReservationEditVM editVM)
        {

            editVM.Users = (await _userService.GetAllAsync()).Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.Id.ToString()));
            editVM.Rooms = (await _roomService.GetAllAsync()).Select(x => new SelectListItem($"{x.RoomType} - {x.Capacity}", x.Id.ToString()));
            return editVM;

        }
    }
}
