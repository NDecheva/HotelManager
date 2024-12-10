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

    public class ClientReservationController : BaseCrudController<ClientReservationDto, IClientReservationRepository, IClientReservationService, ClientReservationEditVM, ClientReservationDetailsVM>
    {
        public IReservationService _reservationService { get; set; }
        public IClientService _clientService { get; set; }

        public ClientReservationController(IClientReservationService service,IReservationService reservationService, IClientService clientService,IMapper mapper) : base(service,mapper)
        {
            this._reservationService = reservationService;
            this._clientService = clientService;
        }

        //protected virtual async Task<ClientReservationEditVM> PrePopulateVMAsync(ClientReservationEditVM editVM)
        //{

        //    editVM.Clients = (await _clientService.GetAllAsync()).Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.Id.ToString()));
        //    editVM.Reservations = (await _reservationService.GetAllAsync()).Select(x => new SelectListItem($"{x.}", x.Id.ToString()));
        //    return editVM;

        //}
    }
}
