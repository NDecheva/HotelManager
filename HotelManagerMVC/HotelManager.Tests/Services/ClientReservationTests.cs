using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Tests.Services
{
    public class ClientReservationTests
    {
        private readonly Mock<IClientReservationRepository> _clientReservationRepositoryMock = new Mock<IClientReservationRepository>();
        private readonly IClientReservationService _service;
        private ClientReservationDto clientReservation;

    }
}
