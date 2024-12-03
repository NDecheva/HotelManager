using HotelManager.Data.Entities;
using HotelManager.Services;
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
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly IReservationService _service;
        private ReservationDto reservation;

        public ReservationServiceTests()
        {
            _service = new ReservationService(_reservationRepositoryMock.Object);
        }
        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var reservationDto = new ReservationDto()
            {
                RoomId = 2,
                UserId = 2,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                HasBreakfast = true,
                IsAllInclusive = true,
                TotalPrice = 66.6m
            };

            //Act
            await _service.SaveAsync(reservationDto);

            //Asert
            _reservationRepositoryMock.Verify(x => x.SaveAsync(reservationDto), Times.Once());



        }

        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _reservationRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
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
            _reservationRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }


        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidBreedId_ThenReturnUser(int reservationId)
        {
            //Arrange
            var reservationDto = new ReservationDto()
            {
                RoomId = 1,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                HasBreakfast = true,
                IsAllInclusive = true,
                TotalPrice = 677.6m
            };
            _reservationRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(reservationId))))
                .ReturnsAsync(reservationDto);
            //Act
            var userResult = await _service.GetByIdIfExistsAsync(reservationId);

            //Assert
            _reservationRepositoryMock.Verify(x => x.GetByIdAsync(reservationId), Times.Once);
            Assert.That(userResult == reservationDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByAsync_WithInvalidBreedId_ThenReturnDefault(int reservationId)
        {
            var reservationDto = (ReservationDto)default;
            _reservationRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(reservationId))))
                .ReturnsAsync(reservationDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(reservationId);

            //Assert
            _reservationRepositoryMock.Verify(x => x.GetByIdAsync(reservationId), Times.Once);
            Assert.That(userResult == reservation);

        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var reservationDto = new ReservationDto
            {
                RoomId = 1,
                UserId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                HasBreakfast = false,
                IsAllInclusive = true,
                TotalPrice = 17.6m

            };
            _reservationRepositoryMock.Setup(s => s.SaveAsync(It.Is<ReservationDto>(x => x.Equals(reservationDto))))
               .Verifiable();
            //Act
            await _service.SaveAsync(reservationDto);

            //Assert
            _reservationRepositoryMock.Verify(x => x.SaveAsync(reservationDto), Times.Once);
        }
        [Test]


        public async Task WhenGetAllAsync_ThenReturnAllModels()
        {
            // Arrange
            var reservatioDto = new List<ReservationDto>();
            _reservationRepositoryMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(reservatioDto);
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _reservationRepositoryMock.Verify(r => r.GetAllAsync());


        }



        [Test]
        [TestCase(5, 1)]
        [TestCase(10, 2)]
        [TestCase(20, 3)]
        public async Task WhenGetWithPaginationAsync_WithValidPageSizeAndPageNumber_ThenReturnPaginatedModels(int pageSize, int pageNumber)
        {
            // Arrange
            var reservatioDto = new List<ReservationDto>();
            _reservationRepositoryMock
        .Setup(cr => cr.GetWithPaginationAsync(pageSize, pageNumber))
        .ReturnsAsync(reservatioDto);

            // Act
            var result = await _service.GetWithPaginationAsync(pageSize, pageNumber);

            // Assert
            _reservationRepositoryMock.Verify(r => r.GetWithPaginationAsync(pageSize, pageNumber), Times.Once);
            Assert.That(result, Is.EquivalentTo(reservatioDto));
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public async Task WhenExistsByIdAsync_WithValidId_ThenReturnExpectedResult(int id, bool exists)
        {
            // Arrange
            _reservationRepositoryMock.Setup(r => r.ExistsByIdAsync(id)).ReturnsAsync(exists);

            // Act
            var result = await _service.ExistsByIdAsync(id);

            // Assert
            _reservationRepositoryMock.Verify(r => r.ExistsByIdAsync(id), Times.Once);
            Assert.That(result, Is.EqualTo(exists));
        }
    }
}

