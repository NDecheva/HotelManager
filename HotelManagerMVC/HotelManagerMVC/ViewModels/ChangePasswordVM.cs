using System;
using System.ComponentModel.DataAnnotations;
namespace HotelManagerMVC.ViewModels
{
    public class ChangePasswordVM
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
