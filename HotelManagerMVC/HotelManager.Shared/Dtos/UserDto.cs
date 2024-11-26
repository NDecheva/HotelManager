using HotelManager.Shared.Enum;
using System;
using System.Collections.Generic;

namespace HotelManager.Shared.Dtos
{
    public class UserDto : BaseModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UCN { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public bool IsActive { get; set; }

        public Role Role { get; set; }

    }
}