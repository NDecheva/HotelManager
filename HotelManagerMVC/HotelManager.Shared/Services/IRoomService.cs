using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Shared.Services
{
    public interface IRoomService : IBaseCrudService<RoomDto, IRoomRepository>
    {
        public Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync();

        public Task UpdateRoomToNotAvailableAsync(int roomId);

    }
}