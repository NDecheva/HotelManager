using AutoMapper;
using HotelManager.Data.Entities;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Data;
using HotelManager.Shared.Attributes;
using System;
[AutoBind]
public class ClientRepository : BaseRepository<Client, ClientDto>, IClientRepository
{
	public ClientRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
	{
	}
}
