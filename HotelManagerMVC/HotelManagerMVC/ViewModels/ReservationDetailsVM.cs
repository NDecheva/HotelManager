﻿using HotelManager.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ReservationDetailsVM : BaseVM
    {
        public ReservationDetailsVM()
        {
            this.ClientReservations = new List<ClientReservationDetailsVM>();
        }
        [Required]
        [DisplayName("Room")]
        public int RoomId { get; set; }
        public RoomDetailsVM Room { get; set; }
        [Required]
        [DisplayName("Username")]
        public int UserId { get; set; }
        public UserDetailsVM User { get; set; }

        [DisplayName("Check In Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Check Out Date")]
        public DateTime CheckOutDate { get; set; }

        [DisplayName("Has Breakfast")]
        public bool HasBreakfast { get; set; }
        [DisplayName("Is All Inclusive")]
        public bool IsAllInclusive { get; set; }
        public virtual List<ClientReservationDetailsVM> ClientReservations { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
