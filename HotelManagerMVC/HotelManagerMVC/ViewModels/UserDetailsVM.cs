using HotelManager.Shared.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelManagerMVC.ViewModels
{
    public class UserDetailsVM : BaseVM
    {
        public string Username { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string UCN { get; set; }
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [DisplayName("Termination Date")]
        [DataType(DataType.Date)]
        public DateTime TerminationDate { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
    }
}
