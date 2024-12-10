using HotelManager.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientReservationEditVM : BaseVM
    {
        [Required]
        public int ClientId { get; set; }
        [Required]

        public int ReservationId { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; }

        public IEnumerable<SelectListItem> Reservations { get; set; }
    }
}
