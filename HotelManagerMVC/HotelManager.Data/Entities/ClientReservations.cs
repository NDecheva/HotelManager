using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class ClientReservations : BaseEntity
    {
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}