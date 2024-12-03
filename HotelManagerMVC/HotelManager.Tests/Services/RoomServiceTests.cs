using HotelManager.Data.Entities;
using HotelManager.Services;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Enum;
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

        public RoomServiceTests()
        {
            _service = new RoomService(_roomRepositoryMock.Object);
        }

        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var roomDto = new RoomDto()
            {

                RoomNumber = 2,
                Capacity = 16,
                IsAvailable = true,
                PricePerNightAdult = 12.2m,
                PricePerNightChild = 8.2m,
                RoomType = RoomType.Double,
                BreakfastPrice = 6.00m,
                AllInclusivePrice = 7.00m
            };

            //Act
            await _service.SaveAsync(roomDto);

            //Asert
            _roomRepositoryMock.Verify(x => x.SaveAsync(roomDto), Times.Once());
        }



        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _roomRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            //Arrange

            //Act
            await _service.DeleteAsync(id);

            //Assert
            _roomRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidBreedId_ThenReturnUser(int roomid)
        {
            //Arrange
            var roomDto = new RoomDto()
            {
                RoomNumber = 8,
                Capacity = 32,
                IsAvailable = true,
                PricePerNightAdult = 12.2m,
                PricePerNightChild = 8.2m,
                RoomType = RoomType.Maisonette,
                BreakfastPrice = 454.00m,
                AllInclusivePrice = 100.0m
            };
            _roomRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(roomid))))
                .ReturnsAsync(roomDto);
            //Act
            var userResult = await _service.GetByIdIfExistsAsync(roomid);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetByIdAsync(roomid), Times.Once);
            Assert.That(userResult == roomDto);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByAsync_WithInvalidBreedId_ThenReturnDefault(int roomId)
        {
            var roomDto = (RoomDto)default;
            _roomRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(roomId))))
                .ReturnsAsync(roomDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(roomId);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetByIdAsync(roomId), Times.Once);
            Assert.That(userResult == room);

        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var roomDto = new RoomDto
            {
                RoomNumber = 2,
                Capacity = 16,
                IsAvailable = true,
                PricePerNightAdult = 12.2m,
                PricePerNightChild = 8.2m,
                RoomType = RoomType.Double,
                BreakfastPrice = 45.00m,
                AllInclusivePrice = 100.0m

            };
            _roomRepositoryMock.Setup(s => s.SaveAsync(It.Is<RoomDto>(x => x.Equals(roomDto))))
               .Verifiable();
            //Act
            await _service.SaveAsync(roomDto);

            //Assert
            _roomRepositoryMock.Verify(x => x.SaveAsync(roomDto), Times.Once);
        }
        [Test]
        public async Task WhenGetAllAsync_ThenReturnAllModels()
        {
            // Arrange
            var roomDto = new List<RoomDto>();
            _roomRepositoryMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(roomDto);
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _roomRepositoryMock.Verify(r => r.GetAllAsync());


        }

        [Test]
        [TestCase(5, 1)]
        [TestCase(10, 2)]
        [TestCase(20, 3)]
        public async Task WhenGetWithPaginationAsync_WithValidPageSizeAndPageNumber_ThenReturnPaginatedModels(int pageSize, int pageNumber)
        {
            // Arrange
            var roomDto = new List<RoomDto>();
            _roomRepositoryMock
        .Setup(cr => cr.GetWithPaginationAsync(pageSize, pageNumber))
        .ReturnsAsync(roomDto);

            // Act
            var result = await _service.GetWithPaginationAsync(pageSize, pageNumber);

            // Assert
            _roomRepositoryMock.Verify(r => r.GetWithPaginationAsync(pageSize, pageNumber), Times.Once);
            Assert.That(result, Is.EquivalentTo(roomDto));
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public async Task WhenExistsByIdAsync_WithValidId_ThenReturnExpectedResult(int id, bool exists)
        {
            // Arrange
            _roomRepositoryMock.Setup(r => r.ExistsByIdAsync(id)).ReturnsAsync(exists);

            // Act
            var result = await _service.ExistsByIdAsync(id);

            // Assert
            _roomRepositoryMock.Verify(r => r.ExistsByIdAsync(id), Times.Once);
            Assert.That(result, Is.EqualTo(exists));
        }
    }
}
