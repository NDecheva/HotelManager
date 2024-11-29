using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[AutoBind]
public class PetVaccineRepository : BaseRepository<PetVaccine, PetVaccineDto>, IPetVaccineRepository
{
	public PetVaccineRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
	{
	}
    public async Task<IEnumerable<PetVaccineDto>> GetAllAsync()
    {

        return this.MapToEnumerableOfModel(await _dbSet.ToListAsync());
    }
    public async Task VacinnatePetAsync(int petId, int vaccineId)
    {
        var pv = new PetVaccineDto(petId, vaccineId);
        await SaveAsync(pv);
    }
}
