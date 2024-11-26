using HotelManager.Shared.Enum;
using System.Collections.Generic;

namespace HotelManager.Shared.Dtos
{
    public class ClientDto : BaseModel
    {
        public ClientDto()
        {
            this.ClientReservations = new List<ClientReservationDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }

        public ICollection<ClientReservationDto> ClientReservations { get; set; }
    }
}