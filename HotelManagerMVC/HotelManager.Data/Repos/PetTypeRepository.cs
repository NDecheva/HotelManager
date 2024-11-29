using AutoMapper;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using System;
[AutoBind]
public class PetTypeRepository : BaseRepository<PetType, PetTypeDto>, IPetTypeRepository
{
	public PetTypeRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
	{
	}
}
