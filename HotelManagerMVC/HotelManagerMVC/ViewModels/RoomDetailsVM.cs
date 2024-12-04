using HotelManager.Data.Entities;
using HotelManager.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class RoomDetailsVM
    {
        public RoomDetailsVM()
        {
            this.Reservations = new List<Reservation>();
        }

        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public decimal PricePerNightAdult { get; set; }
        [Required]
        public decimal PricePerNightChild { get; set; }
        [Required]
        public RoomType RoomType { get; set; }
        [Required]
        public decimal BreakfastPrice { get; set; }
        [Required]
        public decimal AllInclusivePrice { get; set; }
    }
}
