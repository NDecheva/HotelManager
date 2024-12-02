using HotelManager.Services;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Tests.Services
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock = new Mock<IClientRepository>();
        private readonly IClientService _service;
        private ClientDto client;

        public ClientServiceTests()
        {
            _service = new ClientService(_clientRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var clientDto = new ClientDto()
            {
                Id = 0,
                FirstName = "Max",
                LastName = "Maximov",
                PhoneNumber = "43234234234324",
                Email = "maximus123@gmail.com",
                IsAdult = false
            };

            //Act
            await _service.SaveAsync(clientDto);

            //Asert
            _clientRepositoryMock.Verify(x => x.SaveAsync(clientDto), Times.Once());



        }

        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _clientRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
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
            _clientRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }


        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidBreedId_ThenReturnUser(int clientId)
        {
            //Arrange
            var clientDto = new ClientDto()
            {
                Id = 0,
                FirstName = "Maxi",
                LastName = "Maximov",
                PhoneNumber = "1111111111",
                Email = "maximus11123@gmail.com",
                IsAdult = true
            };
            _clientRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(clientId))))
                .ReturnsAsync(clientDto);
            //Act
            var userResult = await _service.GetByIdIfExistsAsync(clientId);

            //Assert
            _clientRepositoryMock.Verify(x => x.GetByIdAsync(clientId), Times.Once);
            Assert.That(userResult == clientDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByAsync_WithInvalidBreedId_ThenReturnDefault(int clientId)
        {
            var clientDto = (ClientDto)default;
            _clientRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(clientId))))
                .ReturnsAsync(clientDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(clientId);

            //Assert
            _clientRepositoryMock.Verify(x => x.GetByIdAsync(clientId), Times.Once);
            Assert.That(userResult == client);

        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var clientDto = new ClientDto
            {
                Id = 0,
                FirstName = "Gabriel",
                LastName = "Praskov",
                PhoneNumber = "0000000000000",
                Email = "halkataGabi111@gmail.com",
                IsAdult = true

            };
            _clientRepositoryMock.Setup(s => s.SaveAsync(It.Is<ClientDto>(x => x.Equals(clientDto))))
               .Verifiable();
            //Act
            await _service.SaveAsync(clientDto);

            //Assert
            _clientRepositoryMock.Verify(x => x.SaveAsync(clientDto), Times.Once);
        }
    }
}

