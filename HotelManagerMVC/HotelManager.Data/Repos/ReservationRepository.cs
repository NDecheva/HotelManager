using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;




[AutoBind]
public class ReservationRepository : BaseRepository<Reservation, ReservationDto>, IReservationRepository

{
    public ReservationRepository(HotelManagerDbContext context, IMapper mapper) : base(context, mapper)
    {

    }
    
}