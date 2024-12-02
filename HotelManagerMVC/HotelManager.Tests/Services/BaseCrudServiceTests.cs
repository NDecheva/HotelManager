using AutoMapper;
using HotelManager.Data.Entities;
using HotelManager.Data;
using HotelManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManager.Shared.Services;
using HotelManager.Shared.Repos.Contracts;
using HotelManager.Services;

namespace HotelManager.Tests.Services
{
    [TestFixture]
    public abstract class BaseCrudServiceTests<TService, T, TModel, TRepository>
    where TService : BaseCrudService<TModel, TRepository>
    where T : class, IBaseEntity
    where TModel : BaseModel
    where TRepository : BaseRepository<T, TModel>
    {
        protected Mock<HotelManagerDbContext> mockContext;
        protected Mock<DbSet<T>> mockDbSet;
        protected Mock<IMapper> mockMapper;
        protected TRepository repository;
        protected TService service;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<HotelManagerDbContext>();
            mockDbSet = new Mock<DbSet<T>>();
            mockMapper = new Mock<IMapper>();
            repository = new Mock<TRepository>(mockContext.Object, mockMapper.Object)
            { CallBase = true }.Object;

            service = CreateService(repository);
        }

        protected abstract TService CreateService(TRepository repository);

        [Test]
        public async Task SaveAsync_ValidModel_CallsRepositorySaveAsync()
        {
            var model = new Mock<TModel>().Object;
            await service.SaveAsync(model);
            Mock.Get(repository).Verify(repo => repo.SaveAsync(It.Is<TModel>(m => m == model)), Times.Once);
        }

        [Test]
        public void SaveAsync_NullModel_ThrowsArgumentNullException()
        {
            TModel model = null;
            Assert.ThrowsAsync<ArgumentNullException>(() => service.SaveAsync(model));
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllModels()
        {
            var models = new Mock<IEnumerable<TModel>>().Object;
            Mock.Get(repository).Setup(repo => repo.GetAllAsync()).ReturnsAsync(models);
            var result = await service.GetAllAsync();
            Assert.That(result, Is.EqualTo(models));
        }

        [Test]
        public async Task DeleteAsync_ValidId_CallsRepositoryDeleteAsync()
        {
            var id = 1;
            await service.DeleteAsync(id);
            Mock.Get(repository).Verify(repo => repo.DeleteAsync(id), Times.Once);
        }

        [Test]
        public async Task GetByIdIfExistsAsync_ValidId_ReturnsModel()
        {
            var id = 1;
            var model = new Mock<TModel>().Object;
            Mock.Get(repository).Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(model);
            var result = await service.GetByIdIfExistsAsync(id);
            Assert.That(result, Is.EqualTo(model));
        }

        [Test]
        public async Task GetByIdIfExistsAsync_InvalidId_ReturnsNull()
        {
            var id = 99;
            Mock.Get(repository).Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((TModel)null);
            var result = await service.GetByIdIfExistsAsync(id);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetWithPaginationAsync_ValidParameters_ReturnsPagedModels()
        {
            var pageSize = 10;
            var pageNumber = 2;
            var models = new Mock<IEnumerable<TModel>>().Object;
            Mock.Get(repository).Setup(repo => repo.GetWithPaginationAsync(pageSize, pageNumber)).ReturnsAsync(models);
            var result = await service.GetWithPaginationAsync(pageSize, pageNumber);
            Assert.That(result, Is.EqualTo(models));
        }

        [Test]
        public async Task GetWithPaginationAsync_InvalidParameters_ThrowsException()
        {
            var pageSize = 0;
            var pageNumber = -1;
            Assert.ThrowsAsync<ArgumentException>(() => service.GetWithPaginationAsync(pageSize, pageNumber));
        }

        [Test]
        public async Task ExistsByIdAsync_ValidId_ReturnsTrue()
        {
            var id = 1;
            Mock.Get(repository).Setup(repo => repo.ExistsByIdAsync(id)).ReturnsAsync(true);
            var result = await service.ExistsByIdAsync(id);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ExistsByIdAsync_InvalidId_ReturnsFalse()
        {
            var id = 99;
            Mock.Get(repository).Setup(repo => repo.ExistsByIdAsync(id)).ReturnsAsync(false);
            var result = await service.ExistsByIdAsync(id);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SaveAsync_CallsMapperCorrectly()
        {
            var entity = new Mock<T>().Object;
            var model = new Mock<TModel>().Object;
            mockMapper.Setup(m => m.Map<TModel>(entity)).Returns(model);
            Mock.Get(repository).Setup(r => r.SaveAsync(model));
            await service.SaveAsync(model);
            mockMapper.Verify(m => m.Map<TModel>(It.IsAny<T>()), Times.Never);
        }

    }


}
