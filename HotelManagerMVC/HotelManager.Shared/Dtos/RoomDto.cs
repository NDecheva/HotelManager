using HotelManager.Shared.Enum;
using System.Collections.Generic;

namespace HotelManager.Shared.Dtos
{
    public class RoomDto : BaseModel
    {
        public RoomDto()
        {
            this.Reservations = new List<ReservationDto>();
        }

        public int RoomNumber { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; }

        public decimal PricePerNightAdult { get; set; }

        public decimal PricePerNightChild { get; set; }

        public RoomType RoomType { get; set; }

        public ICollection<ReservationDto> Reservations { get; set; }

        public decimal BreakfastPrice { get; set; }

        public decimal AllInclusivePrice { get; set; }

    }
}