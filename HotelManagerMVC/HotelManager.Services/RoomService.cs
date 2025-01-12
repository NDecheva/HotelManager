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
        public Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync()
        {
            return this._repository.GetAvailableRoomsAsync();
        }

        public async Task UpdateRoomToNotAvailableAsync(int roomId)
        {

            if (!await ExistsByIdAsync(roomId))
            {
                throw new ArgumentException($"Room with ID {roomId} isn't available.");
            }
            await _repository.UpdateRoomToNotAvailableAsync(roomId);
        }
    }
}
