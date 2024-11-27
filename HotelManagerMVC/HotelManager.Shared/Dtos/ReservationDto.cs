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

        public ICollection<ClientReservationDto> ClientReservations { get; set; }

        public decimal CalculateTotalPrice()
        {
            if (Room == null)
            {
                return 0;
            }

            decimal totalPrice = 0;

            totalPrice += ClientReservations.Sum(cr => cr.Client.IsAdult ? Room.PricePerNightAdult : Room.PricePerNightChild);

            if (HasBreakfast)
            {
                totalPrice += Room.BreakfastPrice;
            }


            if (IsAllInclusive)
            {
                totalPrice += Room.AllInclusivePrice;
            }

            return totalPrice;
        }
    }
}