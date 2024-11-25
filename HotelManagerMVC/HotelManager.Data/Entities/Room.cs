using HotelManager.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class Room : BaseEntity
    {
        public Room()
        {
            this.Reservations = new List<Reservation>();
        }

        public int RoomNumber { get; set; }
        public int Capacity { get; set; } 
        public bool IsAvailable { get; set; } 
        public decimal PricePerNightAdult { get; set; } 
        public decimal PricePerNightChild { get; set; } 
        public RoomType RoomType { get; set; } 
        public virtual ICollection<Reservation> Reservations { get; set; }
        public decimal BreakfastPrice { get; set; } 
        public decimal AllInclusivePrice { get; set; }
    }
}
