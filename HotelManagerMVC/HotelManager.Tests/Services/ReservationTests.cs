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
    public class ReservationTests
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly IReservationService _service;
        private ReservationDto reservation;
    }
}
