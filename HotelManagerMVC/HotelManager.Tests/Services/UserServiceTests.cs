using HotelManager.Data.Entities;
using HotelManager.Services;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Enum;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Security;
using HotelManager.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
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

        public async Task WhenGetByIdAsync_WithValidBreedId_ThenReturnUser(int userid)
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
                HireDate = DateTime.Now,
                TerminationDate = DateTime.UtcNow,
                IsActive = true,
                Role = Role.Admin
            };
            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(userid))))
                .ReturnsAsync(userDto);
            //Act
            var userResult = await _service.GetByIdIfExistsAsync(userid);

            //Assert
            _userRepositoryMock.Verify(x => x.GetByIdAsync(userid), Times.Once);
            Assert.That(userResult == userDto);
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

        [Test]
        public async Task WhenGetAllAsync_ThenReturnAllModels()
        {
            // Arrange
            var userDto = new List<UserDto>();
            _userRepositoryMock.Setup(s => s.GetAllAsync())
                .ReturnsAsync(userDto);
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _userRepositoryMock.Verify(r => r.GetAllAsync());


        }



        [Test]
        [TestCase(5, 1)]
        [TestCase(10, 2)]
        [TestCase(20, 3)]
        public async Task WhenGetWithPaginationAsync_WithValidPageSizeAndPageNumber_ThenReturnPaginatedModels(int pageSize, int pageNumber)
        {
            // Arrange
            var userDto = new List<UserDto>();
            _userRepositoryMock
        .Setup(cr => cr.GetWithPaginationAsync(pageSize, pageNumber))
        .ReturnsAsync(userDto);

            // Act
            var result = await _service.GetWithPaginationAsync(pageSize, pageNumber);

            // Assert
            _userRepositoryMock.Verify(r => r.GetWithPaginationAsync(pageSize, pageNumber), Times.Once);
            Assert.That(result, Is.EquivalentTo(userDto));
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public async Task WhenExistsByIdAsync_WithValidId_ThenReturnExpectedResult(int id, bool exists)
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.ExistsByIdAsync(id)).ReturnsAsync(exists);

            // Act
            var result = await _service.ExistsByIdAsync(id);

            // Assert
            _userRepositoryMock.Verify(r => r.ExistsByIdAsync(id), Times.Once);
            Assert.That(result, Is.EqualTo(exists));
        }
        


        [Test]
        public void HashPassword_And_VerifyPassword_ReturnsTrue_ForCorrectPassword()
        {
            // Arrange
            string password = "petkovkatisthebest";

            // Act
            string hashedPassword = PasswordHasher.HashPassword(password);
            bool isVerified = PasswordHasher.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.That(isVerified);
        }

        [Test]
        public void VerifyPassword_ReturnsFalse_ForIncorrectPassword()
        {
            // Arrange
            string password = "petvokataisthebest";
            string incorrectPassword = "petkovkataisntthebest";

            string hashedPassword = PasswordHasher.HashPassword(password);

            // Act
            bool isVerified = PasswordHasher.VerifyPassword(incorrectPassword, hashedPassword);

            // Assert
            Assert.False(isVerified);
        }

        [Test]
        public void VerifyPassword_ReturnsFalse_ForNullHashedPassword()
        {
            // Arrange
            string password = "rabotanebiva";

            // Act
            bool isVerified = PasswordHasher.VerifyPassword(password, null);

            // Assert
            Assert.False(isVerified);
        }

        [Test]
        public void HashPassword_ReturnsDifferentHashes_ForDifferentPasswords()
        {
            // Arrange
            string password1 = "petkovkathebest";
            string password2 = "petkovkataisntthebest";

            // Act
            string hashedPassword1 = PasswordHasher.HashPassword(password1);
            string hashedPassword2 = PasswordHasher.HashPassword(password2);

            // Assert
            Assert.NotEqual(hashedPassword1, hashedPassword2);
        }




    }

}
