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
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _roomRepositoryMock = new Mock<IRoomRepository>();
        private readonly IRoomService _service;
        private RoomDto room;
    }
}
