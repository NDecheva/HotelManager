using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientReservationEditVM : BaseVM
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ReservationId { get; set; }
    }
}
