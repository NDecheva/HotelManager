using HotelManager.Data.Entities;
using HotelManager.Shared.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class RoomDetailsVM : BaseVM
    {

        public RoomDetailsVM()
        {
            this.Reservations = new List<ReservationDetailsVM>();
        }
        [DisplayName("Room Number")]
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        [DisplayName("Is Available")]
        public bool IsAvailable { get; set; }

        public RoomType RoomType { get; set; }

        [DisplayName("Price Per Night For Adult")]
        public decimal PricePerNightAdult { get; set; }
        [DisplayName("Price Per Night For Child")]
        public decimal PricePerNightChild { get; set; }

        [DisplayName("Breakfast Price")]
        public decimal BreakfastPrice { get; set; }

        [DisplayName("All Inclusive Price")]
        public decimal AllInclusivePrice { get; set; }

        public virtual List<ReservationDetailsVM> Reservations { get; set; }
    }
}
