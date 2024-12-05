using HotelManager.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ReservationEditVM : BaseVM
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]

        [DisplayName("Check In Date")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [Required]

        [DisplayName("Check Out Date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [DisplayName("Has Breakfast")]
        public bool? HasBreakfast { get; set; }
        [Required]
        [DisplayName("Is All Inclusive")]
        public bool? IsAllInclusive { get; set; }
        [Required]
        public IEnumerable<SelectListItem> User { get; set; }
    }
}
