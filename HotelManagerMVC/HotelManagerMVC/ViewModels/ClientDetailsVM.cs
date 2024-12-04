using HotelManager.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class ClientDetailsVM
    {
       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool IsAdult { get; set; }
    }
}
