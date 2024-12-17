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
        private DbContextOptions<HotelManagerDbContext> _dbContextOptions;
        private IMapper mapper;
      

        [Test]
        public async Task GetByUsernameAsync_UserExists_ReturnsUserDto()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>().ReverseMap();
            });

            mapper = mapperConfig.CreateMapper();

            var _dbContextOptions = new DbContextOptionsBuilder<HotelManagerDbContext>()
                .UseInMemoryDatabase("HotelManagerMVC")
                .Options;

            // Arrange
            var username = "testuser";
            var user = new User { Id = 1, Username = username, FirstName = "Petur", Email="nababati",MiddleName="nqmaime",PhoneNumber="823324324324", UCN="888888888", LastName = "Example", Password = "password" };
            var userDto = new UserDto { Id = 1, Username = username, FirstName = "Petur", Email = "nababati", MiddleName = "nqmaime", PhoneNumber = "823324324324", UCN = "888888888", LastName = "Example", Password = "password" };
            using (var context = new HotelManagerDbContext(_dbContextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }


            // Act
            UserDto result;
            using (var context = new HotelManagerDbContext(_dbContextOptions))
            {
                var _userRepositoryMock = new UserRepository(context, mapper);
                result = await _userRepositoryMock.GetByUsernameAsync(username);
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
