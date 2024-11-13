using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public List<Client> Clients { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public bool HasBreakfast { get; set; }
        public bool IsAllInclusive { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();

    }
}
