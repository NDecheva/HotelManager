using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using HotelManager.Data.Entities;
using HotelManager.Shared.Dtos;
using HotelManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace HotelManager.Tests
{
    public abstract class BaseRepositoryTests<TRepository, T, TModel>
            where TRepository : BaseRepository<T, TModel>
            where T : class, IBaseEntity
            where TModel : BaseModel
    {
        private Mock<HotelManagerDbContext> mockContext;
        private Mock<DbSet<T>> mockDbSet;
        private Mock<IMapper> mockMapper;
        private TRepository repository;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<HotelManagerDbContext>();
            mockDbSet = new Mock<DbSet<T>>();
            mockMapper = new Mock<IMapper>();
            repository = new Mock<TRepository>(mockContext.Object, mockMapper.Object)
            { CallBase = true }.Object;
        }

        [Test]
        public void MapToModel_ValidEntity_ReturnsMappedModel()
        {
            var entity = new Mock<T>();
            var model = new Mock<TModel>();
            mockMapper.Setup(m => m.Map<TModel>(entity.Object)).Returns(model.Object);

            var result = repository.MapToModel(entity.Object);

            Assert.That(result, Is.EqualTo(model.Object));
        }

        [Test]
        public void MapToModel_NullEntity_ReturnsNull()
        {
            T entity = null;

            var result = repository.MapToModel(entity);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void MapToEntity_ValidModel_ReturnsMappedEntity()
        {
            var model = new Mock<TModel>();
            var entity = new Mock<T>();
            mockMapper.Setup(m => m.Map<T>(model.Object)).Returns(entity.Object);

            var result = repository.MapToEntity(model.Object);

            Assert.That(result, Is.EqualTo(entity.Object));
        }

        [Test]
        public void MapToEntity_NullModel_ReturnsNull()
        {
            TModel model = null;

            var result = repository.MapToEntity(model);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void MapToEnumerableOfModel_ValidEntities_ReturnsMappedModels()
        {
            var entities = new Mock<IEnumerable<T>>();
            var models = new Mock<IEnumerable<TModel>>();
            mockMapper.Setup(m => m.Map<IEnumerable<TModel>>(entities.Object)).Returns(models.Object);

            var result = repository.MapToEnumerableOfModel(entities.Object);

            Assert.That(result, Is.EqualTo(models.Object));
        }

        [Test]
        public void MapToEnumerableOfModel_NullEntities_ReturnsNull()
        {
            IEnumerable<T> entities = null;

            var result = repository.MapToEnumerableOfModel(entities);

            Assert.That(result, Is.EqualTo(null));
        }

        // Test GetAllAsync Method
        [Test]
        public async Task GetAllAsync_ReturnsMappedModels()
        {
            var entities = new List<T> { new Mock<T>().Object, new Mock<T>().Object };
            var models = new List<TModel> { new Mock<TModel>().Object, new Mock<TModel>().Object };

            mockDbSet.As<IQueryable<T>>().Setup(m => m.ToListAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(entities);
            mockMapper.Setup(m => m.Map<IEnumerable<TModel>>(entities)).Returns(models);

            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var result = await repository.GetAllAsync();

            Assert.That(result, Is.EqualTo(models));
        }

        // Test GetByIdAsync Method
        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsMappedModel()
        {
            var entity = new Mock<T>();
            var model = new Mock<TModel>();
            mockDbSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(entity.Object);
            mockMapper.Setup(m => m.Map<TModel>(entity.Object)).Returns(model.Object);

            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var result = await repository.GetByIdAsync(1);

            Assert.That(result, Is.EqualTo(model.Object));
        }

        [Test]
        public async Task GetByIdAsync_EntityNotFound_ThrowsArgumentNullException()
        {
            mockDbSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync((T)null);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.GetByIdAsync(1));

            Assert.That(ex.Message, Contains.Substring("entity"));
        }

        // Test CreateAsync Method
        [Test]
        public async Task CreateAsync_ValidModel_AddsEntityToDbSet()
        {
            var model = new Mock<TModel>();
            var entity = new Mock<T>();
            mockMapper.Setup(m => m.Map<T>(model.Object)).Returns(entity.Object);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            await repository.CreateAsync(model.Object);

            mockDbSet.Verify(m => m.AddAsync(entity.Object, It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        // Test UpdateAsync Method
        [Test]
        public async Task UpdateAsync_ValidModel_UpdatesEntityInDbSet()
        {
            var model = new Mock<TModel> { CallBase = true };
            model.Setup(m => m.Id).Returns(1);
            var entity = new Mock<T>();
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(entity.Object);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            await repository.UpdateAsync(model.Object);

            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        // Test DeleteAsync Method
        [Test]
        public async Task DeleteAsync_ValidId_RemovesEntityFromDbSet()
        {
            var entity = new Mock<T>();
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(entity.Object);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            await repository.DeleteAsync(1);

            mockDbSet.Verify(m => m.Remove(entity.Object), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_EntityNotFound_ThrowsArgumentNullException()
        {
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync((T)null);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.DeleteAsync(1));

            Assert.That(ex.Message, Contains.Substring("entity"));
        }

        // Test ExistsByIdAsync Method
        [Test]
        public async Task ExistsByIdAsync_EntityExists_ReturnsTrue()
        {
            // Correcting the setup to use Expression<Func<T, bool>>
            mockDbSet.Setup(m => m.AnyAsync(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(true);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var result = await repository.ExistsByIdAsync(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ExistsByIdAsync_EntityNotFound_ReturnsFalse()
        {
            // Correcting the setup to use Expression<Func<T, bool>>
            mockDbSet.Setup(m => m.AnyAsync(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(false);
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var result = await repository.ExistsByIdAsync(1);

            Assert.That(result, Is.False);
        }

        // Test GetWithPaginationAsync Method
        [Test]
        public async Task GetWithPaginationAsync_ReturnsPagedResults()
        {
            var entities = new List<T> { new Mock<T>().Object, new Mock<T>().Object };
            var models = new List<TModel> { new Mock<TModel>().Object, new Mock<TModel>().Object };

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Skip(It.IsAny<int>())).Returns(mockDbSet.Object);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Take(It.IsAny<int>())).Returns(mockDbSet.Object);
            mockDbSet.Setup(m => m.ToListAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(entities);
            mockMapper.Setup(m => m.Map<IEnumerable<TModel>>(entities)).Returns(models);

            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);

            var result = await repository.GetWithPaginationAsync(2, 1);

            Assert.That(result, Is.EqualTo(models));
        }
    }
}
