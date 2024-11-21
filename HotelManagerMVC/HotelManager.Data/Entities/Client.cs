using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Entities
{
    public class Client : BaseEntity
    {
        public Client()
        {
            this.Reservations = new List<Reservation>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; } 
        public string Email { get; set; }
        public bool IsAdult { get; set; } 
        public virtual ICollection<Reservation> Reservations { get; set; } 
    }
}

