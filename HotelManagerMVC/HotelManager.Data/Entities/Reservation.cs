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
            this.Clients = new List<Client>();
        }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public bool HasBreakfast { get; set; }
        public bool IsAllInclusive { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}

