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
    

        public ClientReservationController(IClientReservationService service, IMapper mapper) : base(service,mapper)
        {
            
        }

        
    }
}
