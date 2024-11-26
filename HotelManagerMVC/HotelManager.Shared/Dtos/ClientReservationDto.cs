using HotelManager.Shared.Enum;
using System.Collections.Generic;

namespace HotelManager.Shared.Dtos
{
    public class ClientReservationDto : BaseModel
    {
        public int ClientId { get; set; }

        public ClientDto Client { get; set; }

        public int ReservationId { get; set; }

        public ReservationDto Reservation { get; set; }
    }
}