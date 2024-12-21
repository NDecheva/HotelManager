using AutoMapper;
using HotelManager.Data;
using HotelManager.Data.Entities;
using HotelManager.Services;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NuGet.Protocol.Core.Types;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Tests.Repos
{
    public class UserRepositoryTests : BaseRepositoryTests<UserRepository, User, UserDto>
    {
        private readonly DbContextOptions<HotelManagerDbContext> _dbContextOptions;

        public UserRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<HotelManagerDbContext>()
                .UseInMemoryDatabase("HotelManagerMVC")
                .Options;
        }

        [Test]
        public async Task GetByUsernameAsync_UserExists_ReturnsUserDto()
        {
            // Arrange
            var username = "petkovkata";
            var user = new User
            {
                Id = 1,
                Username = username,
                FirstName = "Petur",
                Email = "nababati",
                MiddleName = "nqmaime",
                PhoneNumber = "823324324324",
                UCN = "888888888",
                LastName = "Example",
                Password = "password"
            };
            var userDto = new UserDto
            {
                Id = 1,
                Username = username,
                FirstName = "Petur",
                Email = "nababati",
                MiddleName = "nqmaime",
                PhoneNumber = "823324324324",
                UCN = "888888888",
                LastName = "Example",
                Password = "password"
            };

            mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);

            using (var context = new HotelManagerDbContext(_dbContextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Act
            UserDto result;
            using (var context = new HotelManagerDbContext(_dbContextOptions))
            {
                var userRepository = new UserRepository(context, mockMapper.Object);
                result = await userRepository.GetByUsernameAsync(username);
            }

            // Assert
            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(username, result.Username);
            ClassicAssert.AreEqual(userDto.FirstName, result.FirstName);
            ClassicAssert.AreEqual(userDto.LastName, result.LastName);
            ClassicAssert.AreEqual(userDto.Password, result.Password);
        }
    }
}
