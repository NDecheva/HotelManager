using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientReservationDetailsVM : BaseVM
    {
        public int ClientId { get; set; }
        public int ReservationId { get; set; }
        public virtual ReservationDetailsVM Reservation { get; set; }
        public virtual ClientDetailsVM Client { get; set; }
    }
}
