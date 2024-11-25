﻿using System;
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
            this.ClientReservation = new List<ClientReservations>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public bool IsAdult { get; set; }
        public virtual ICollection<ClientReservations> ClientReservation { get; set; }
    }
}

