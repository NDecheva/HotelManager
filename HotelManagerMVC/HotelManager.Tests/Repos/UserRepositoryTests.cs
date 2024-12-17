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
        private readonly IUserRepository _userRepositoryMock;
        private Mock<HotelManagerDbContext> mockContext;
        private Mock<DbSet<User>> mockDbSet;
        private Mock<IMapper> mockMapper;
        private UserDto user;
        public UserRepositoryTests()
        {
            mockContext = new Mock<HotelManagerDbContext>();
            mockDbSet = new Mock<DbSet<User>>();
            mockMapper = new Mock<IMapper>();
        }



        [Test]
        public async Task GetByUsernameAsync_UserNotFound_ReturnsNull()
        {
            // Arrange
            string username = "petkovkata";

            mockDbSet.Setup(db => db.FirstOrDefaultAsync(It.Is<Expression<User, bool>(expr => expr.Compile().Invoke(new User { Username = username }))))
                     .ReturnsAsync((User)null);

            // Act
            var result = await _userRepositoryMock.GetByUsernameAsync(username);

            // Assert
            Assert.That(result, Is.Null);
        }


    }
}
