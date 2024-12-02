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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly IUserService _service;
        private UserDto user;

        public UserServiceTests()
        {
            _service = new UserService(_userRepositoryMock.Object);
        }

        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var userDto = new UserDto()
            {

                Username = "petkovkata",
                Password = "nqma da ti kaja",
                FirstName = "Kristian",
                MiddleName = "Andonov",
                LastName = "Petkov",
                UCN = "0651228454",
                Email = "callmebaby69@gmail.com",
                PhoneNumber = "777 777 7777",
                HireDate = DateTime.Today,
                TerminationDate = DateTime.UtcNow,
                IsActive = true,
                Role = Role.Admin
            };

            //Act
            await _service.SaveAsync(userDto);

            //Asert
            _userRepositoryMock.Verify(x => x.SaveAsync(userDto), Times.Once());
        }



        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _userRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
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
            _userRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByAsync_WithInvalidBreedId_ThenReturnDefault(int userId)
        {
            var userDto = (UserDto)default;
            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(userId))))
                .ReturnsAsync(userDto);

            //Act
            var userResult = await _service.GetByIdIfExistsAsync(userId);

            //Assert
            _userRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once);
            Assert.That(userResult == user);

        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var userDto = new UserDto
            {
                Username = "petkovkata",
                Password = "nqma da ti kaja",
                FirstName = "Kristian",
                MiddleName = "Andonov",
                LastName = "Petkov",
                UCN = "0651228454",
                Email = "callmebaby69@gmail.com",
                PhoneNumber = "777 777 7777",
                HireDate = DateTime.Today,
                TerminationDate = DateTime.UtcNow,
                IsActive = true,
                Role = Role.Admin

            };
            _userRepositoryMock.Setup(s => s.SaveAsync(It.Is<UserDto>(x => x.Equals(userDto))))
               .Verifiable();
            //Act
            await _service.SaveAsync(userDto);

            //Assert
            _userRepositoryMock.Verify(x => x.SaveAsync(userDto), Times.Once);
        }
    }

}
