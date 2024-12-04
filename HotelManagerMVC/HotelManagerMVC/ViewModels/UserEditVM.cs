using HotelManager.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class UserEditVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UCN { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime HireDate { get; set; }
        [Required]
        public DateTime? TerminationDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
