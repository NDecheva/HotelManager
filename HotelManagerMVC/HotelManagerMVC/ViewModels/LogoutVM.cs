using HotelManagerMVC.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class LogoutVM : BaseVM
    {
        [Required]
        public string Message { get; set; }
    }
}