using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ReservationDetailsVM
    {
        [Required]
        public int RoomId { get; set; }
        [Required]
        public virtual Room Room { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public bool HasBreakfast { get; set; }
        [Required]
        public bool IsAllInclusive { get; set; }
    }
}
