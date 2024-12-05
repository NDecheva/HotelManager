using HotelManager.Data.Entities;
using HotelManager.Shared.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class RoomEditVM : BaseVM
    {
        [Required]
        [DisplayName("Room Number")]
        public int RoomNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        [DisplayName("Is Available")]
        public bool? IsAvailable { get; set; }
        [Required]
        [DisplayName("Price Per Night For Adult")]
        public decimal PricePerNightAdult { get; set; }
        [Required]
        [DisplayName("Price Per Night For Child")]
        public decimal PricePerNightChild { get; set; }
        [Required]
        public RoomType RoomType { get; set; }
        [Required]
        public virtual ICollection<Reservation> Reservations { get; set; }
        [Required]
        [DisplayName("Breakfast Price")]
        public decimal BreakfastPrice { get; set; }
        [Required]
        [DisplayName("All Inclusive Price")]
        public decimal AllInclusivePrice { get; set; }
    }
}
