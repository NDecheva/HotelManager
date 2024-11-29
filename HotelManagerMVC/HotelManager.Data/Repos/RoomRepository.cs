using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using System;
using System.Threading.Tasks;
using HotelManager.Shared.Repos.Contracts;

[AutoBind]
public class RoomRepository : BaseRepository<Room, RoomDto>, IRoomRepository
{
	public RoomRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
	{

	}
}
