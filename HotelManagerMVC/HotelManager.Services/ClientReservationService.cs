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
    public class ClientReservationService : BaseCrudService<ClientReservationDto, IClientReservationRepository>, IClientReservationService
    {
        public ClientReservationService(IClientReservationRepository repository) : base(repository)
        {

        }
    }
}
