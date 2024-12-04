using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientReservationDetailsVM
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public virtual Client Client { get; set; }
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public virtual Reservation Reservation { get; set; }
    }
}
