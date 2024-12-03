using HotelManager.Shared.Attributes;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Services
{
    [AutoBind]
    public class ReservationService : BaseCrudService<ReservationDto, IReservationRepository>, IReservationService
    {
        public ReservationService(IReservationRepository repository) : base(repository)
        {

        }
    }
}
