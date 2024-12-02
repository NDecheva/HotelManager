using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManager.Shared.Repos.Contracts;
[AutoBind]
public class ClientReservationRepository : BaseRepository<ClientReservation, ClientReservationDto>, IClientReservationRepository
{
	public ClientReservationRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
	{

	}
}
