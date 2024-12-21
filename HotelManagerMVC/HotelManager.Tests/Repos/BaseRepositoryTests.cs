using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using HotelManager.Data.Entities;
using HotelManager.Shared.Dtos;
using HotelManager.Data;

namespace HotelManager.Tests
{
    public abstract class BaseRepositoryTests<TRepository, T, TModel>
            where TRepository : BaseRepository<T, TModel>
            where T : class, IBaseEntity
            where TModel : BaseModel
    {
        protected Mock<HotelManagerDbContext> mockContext;
        protected Mock<DbSet<T>> mockDbSet;
        protected Mock<IMapper> mockMapper;
        protected TRepository repository;

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
            //Arrange
            var entity = new Mock<T>();
            var model = new Mock<TModel>();
            mockMapper.Setup(m => m.Map<TModel>(entity.Object)).Returns(model.Object);

            //Act
            var result = repository.MapToModel(entity.Object);

            //Assert
            Assert.That(result, Is.EqualTo(model.Object));
        }

        [Test]
        public void MapToModel_NullEntity_ReturnsNull()
        {
            //Arrange
            T entity = null;

            //Act
            var result = repository.MapToModel(entity);

            //Assert
            Assert.That(result, Is.EqualTo(null));

        }

        [Test]
        public void MapToEntity()
        {
            //Arrange
            var model = new Mock<TModel>();

            //Act
            var result = repository.MapToEntity(model.Object);

            //Assert
            Assert.That(result, Is.EqualTo(null));
        }


        [Test]
        public void MapToEnumerableOfModel()
        {
            var entity = new Mock<IEnumerable<T>>();
            var model = new Mock<IEnumerable<TModel>>();
            mockMapper.Setup(m => m.Map<IEnumerable<TModel>>(entity.Object)).Returns(model.Object);

            //Act
            var result = repository.MapToEnumerableOfModel(entity.Object);

            //Assert
            Assert.That(result, Is.EqualTo(model.Object));
        }
    }
}