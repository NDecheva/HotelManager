using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class UserReservation : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}