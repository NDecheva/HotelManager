using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class ClientRoom : BaseEntity
    {
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

    }
}
