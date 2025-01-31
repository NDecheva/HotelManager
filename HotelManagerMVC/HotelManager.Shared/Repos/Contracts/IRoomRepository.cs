using HotelManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Shared.Repos.Contracts
{
    public interface IRoomRepository : IBaseRepository<RoomDto>
    {
        public Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync();
        public Task UpdateRoomToNotAvailableAsync(int roomId);

    }
}
