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
    public class RoomService : BaseCrudService<RoomDto, IRoomRepository>, IRoomService

    {
        public RoomService(IRoomRepository repository) : base(repository)
        {

        }
    }
}
