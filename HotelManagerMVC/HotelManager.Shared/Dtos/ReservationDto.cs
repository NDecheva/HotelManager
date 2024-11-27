using HotelManager.Shared.Enum;
using System;
using System.Collections.Generic;

namespace HotelManager.Shared.Dtos
{
    public class ReservationDto : BaseModel
    {
        public ReservationDto()
        {
            this.ClientReservations = new List<ClientReservationDto>();
        }

        public int RoomId { get; set; }

        public RoomDto Room { get; set; }

        public int UserId { get; set; }

        public UserDto User { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public bool HasBreakfast { get; set; }

        public bool IsAllInclusive { get; set; }

        public decimal TotalPrice { get; private set; }

        public virtual ICollection<ClientReservationDto> ClientReservations { get; set; }

        
    }
}