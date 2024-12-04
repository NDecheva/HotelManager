using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientReservationEditVM
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ReservationId { get; set; }
    }
}
