using AutoMapper;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using HotelManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Sockets;
using System.Security.Claims;

namespace HotelManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin, User")]
    public class ReservationController : BaseCrudController<ReservationDto, IReservationRepository, IReservationService, ReservationEditVM, ReservationDetailsVM>
    {
        private readonly IClientService _clientService;
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;

        public ReservationController(IReservationService service, IClientService _clientService, IRoomService _roomService, IMapper mapper, IUserService _userService) : base(service, mapper)
        {
            this._clientService = _clientService;
            this._roomService = _roomService;
            this._userService = _userService;
        }

        protected override async Task<ReservationEditVM> PrePopulateVMAsync(ReservationEditVM editVM)
        {

            editVM.Clients = (await _clientService.GetAllAsync()).Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.Id.ToString()));
            editVM.Rooms = (await _roomService.GetAvailableRoomsAsync()).Select(x => new SelectListItem($"Room number({x.RoomNumber}) - {x.RoomType}", x.Id.ToString()));
            return editVM;

        }

        public override async Task<IActionResult> Create(ReservationEditVM editVM)
        {
            string loggedUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await this._userService.GetByUsernameAsync(loggedUsername);
            editVM.ClientReservations = new List<ClientReservationEditVM>();
            AddClient(editVM);
            editVM.UserId = user.Id;

            await UpdateRoomToNotAvailableAsync(editVM);

            return await base.Create(editVM);
        }

        public void AddClient(ReservationEditVM editVM)
        {
            foreach (var clientId in editVM.ClientsIds)
            {
                editVM.ClientReservations.Add(new ClientReservationEditVM { ClientId = clientId });
            }
        }
        public async Task UpdateRoomToNotAvailableAsync(ReservationEditVM editVM)
        {
            int roomId = editVM.RoomId;
            await _roomService.UpdateRoomToNotAvailableAsync(roomId);


        }

    }
}
