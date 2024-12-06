using HotelManager.Shared.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class UserEditVM : BaseVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string UCN { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [Required]
        [DisplayName("Termination Date")]
        [DataType(DataType.Date)]
        public DateTime TerminationDate { get; set; }
        [Required]
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}

