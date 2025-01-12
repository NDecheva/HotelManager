using HotelManager.Data.Entities;
using HotelManager.Shared.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ReservationEditVM : BaseVM
    {
        [Required]
        [DisplayName("Room")]
        public int RoomId { get; set; }


        [Required]
        [DisplayName("User")]
        public int UserId { get; set; }
        [Required]

        [DisplayName("Check In Date")]
        [DataType(DataType.Date)]
        public DateTime? CheckInDate { get; set; }
        [Required]

        [DisplayName("Check Out Date")]
        [DataType(DataType.Date)]
        public DateTime? CheckOutDate { get; set; }
        [Required]
        [DisplayName("Has Breakfast")]
        public bool HasBreakfast { get; set; }
        [Required]
        [DisplayName("Is All Inclusive")]
        public bool IsAllInclusive { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }

        [Required]
        [DisplayName("Clients")]
        public List<int> ClientsIds { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; }

        public ICollection<ClientReservationEditVM> ClientReservations { get; set; }


    }
}
