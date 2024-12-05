using HotelManager.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientDetailsVM : BaseVM
    {

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [DisplayName("Is Adult")]
        public bool? IsAdult { get; set; }

        public virtual List<ClientReservationDetailsVM> ClientReservation { get; set; }
    }
}
