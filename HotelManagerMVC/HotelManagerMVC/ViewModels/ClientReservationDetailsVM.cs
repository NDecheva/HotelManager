using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientReservationDetailsVM : BaseVM
    {
        public int ClientId { get; set; }
        public int ReservationId { get; set; }
        public  ReservationDetailsVM Reservations { get; set; }
        public  ClientDetailsVM Clients { get; set; }
    }
}
