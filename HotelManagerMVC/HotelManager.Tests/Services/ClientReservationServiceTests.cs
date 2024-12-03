using HotelManager.Data.Entities;
using HotelManager.Services;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Tests.Services
{
    public class ClientReservationServiceTests
    {
        private readonly Mock<IClientReservationRepository> _clientReservationRepositoryMock = new Mock<IClientReservationRepository>();
        private readonly IClientReservationService _service;
        private ClientReservationDto clientReservation;

        public ClientReservationServiceTests()
        {
            _service = new ClientReservationService(_clientReservationRepositoryMock.Object);
        }
        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var clientReservationDto = new ClientReservationDto()
            {

                ClientId = 1,
                ReservationId = 2

            };

            //Act
            await _service.SaveAsync(clientReservationDto);

            //Asert
            _clientReservationRepositoryMock.Verify(x => x.SaveAsync(clientReservationDto), Times.Once());
        }



        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _clientReservationRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
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
            _clientReservationRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }



        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidBreedId_ThenReturnUser(int clientreservationid)
        {
            //Arrange
            var clientReservationDto = new ClientReservationDto()
            {
                ClientId = 3,
                ReservationId = 3
            };
            _clientReservationRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(clientreservationid))))
                .ReturnsAsync(clientReservationDto);
            //Act
            var userResult = await _service.GetByIdIfExistsAsync(clientreservationid);

            //Assert
            _clientReservationRepositoryMock.Verify(x => x.GetByIdAsync(clientreservationid), Times.Once);
            Assert.That(userResult == clientReservationDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByAsync_WithInvalidBreedId_ThenReturnDefault(int clientreservationid)
        {
            var clientReservationDto = (ClientReservationDto)default;
            _clientReservationRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(clientreservationid))))
                .ReturnsAsync(clientReservationDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(clientreservationid);

            //Assert
            _clientReservationRepositoryMock.Verify(x => x.GetByIdAsync(clientreservationid), Times.Once);
            Assert.That(userResult == clientReservation);

        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var clientReservationDto = new ClientReservationDto
            {
                ClientId = 1,
                ReservationId = 2

            };
            _clientReservationRepositoryMock.Setup(s => s.SaveAsync(It.Is<ClientReservationDto>(x => x.Equals(clientReservationDto))))
               .Verifiable();
            //Act
            await _service.SaveAsync(clientReservationDto);

            //Assert
            _clientReservationRepositoryMock.Verify(x => x.SaveAsync(clientReservationDto), Times.Once);
        }

        [Test]
        public async Task WhenGetAllAsync_ThenReturnAllModels()
        {
            // Arrange
            var clientReservationDto = new List<ClientReservationDto>();
            _clientReservationRepositoryMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(clientReservationDto);
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _clientReservationRepositoryMock.Verify(r => r.GetAllAsync());

         
        }



        [Test]
        [TestCase(5, 1)]
        [TestCase(10, 2)]
        [TestCase(20, 3)]
        public async Task WhenGetWithPaginationAsync_WithValidPageSizeAndPageNumber_ThenReturnPaginatedModels(int pageSize, int pageNumber)
        {
            // Arrange
            var clientReservationDto = new List<ClientReservationDto>();
            _clientReservationRepositoryMock
        .Setup(cr => cr.GetWithPaginationAsync(pageSize, pageNumber))
        .ReturnsAsync(clientReservationDto);

            // Act
            var result = await _service.GetWithPaginationAsync(pageSize, pageNumber);

            // Assert
            _clientReservationRepositoryMock.Verify(r => r.GetWithPaginationAsync(pageSize, pageNumber), Times.Once);
            Assert.That(result, Is.EquivalentTo(clientReservationDto));
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public async Task WhenExistsByIdAsync_WithValidId_ThenReturnExpectedResult(int id, bool exists)
        {
            // Arrange
            _clientReservationRepositoryMock.Setup(r => r.ExistsByIdAsync(id)).ReturnsAsync(exists);

            // Act
            var result = await _service.ExistsByIdAsync(id);

            // Assert
            _clientReservationRepositoryMock.Verify(r => r.ExistsByIdAsync(id), Times.Once);
            Assert.That(result, Is.EqualTo(exists));
        }


    }
}
