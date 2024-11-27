using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public Reservation()
        {
            this.ClientReservations = new List<ClientReservation>();
        }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; } 

        public int UserId { get; set; }
        public virtual User User { get; set; } 

        public DateTime CheckInDate { get; set; } 
        public DateTime CheckOutDate { get; set; } 

        public bool HasBreakfast { get; set; } 
        public bool IsAllInclusive { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return CalculateTotalPrice();
            }
            
        }

        public virtual ICollection<ClientReservation> ClientReservations { get; set; }

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

