using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace HotelManagerMVC.ViewModels
{
    public class ChangePasswordVM
    {
        [Required]
        public string Username { get; set; }
        [Required]

        public string Password { get; set; }

        [Required]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }
    }
}
