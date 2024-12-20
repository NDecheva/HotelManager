﻿using AutoMapper;
using HotelManager.Services;
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

    public class ClientController : BaseCrudController<ClientDto, IClientRepository, IClientService, ClientEditVM, ClientDetailsVM>
    {


        public ClientController(IClientService service, IMapper mapper) : base(service, mapper)
        {
            
        }
        

    }
}
