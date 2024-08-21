using AutoMapper;
using MongoDB.Bson;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infra.Mongo.DataModel;
using ProductCatalog.Infra.Mongo.Repositories;
using Xunit;

namespace ProductCatalog.Tests.Integration
{
    public class SupplierRepositoryTests : IClassFixture<MongoDbFixture>
    {
        private readonly SupplierRepository _repository;
        private readonly IMapper _mapper;
        private readonly Domain.Ports.IMapper _customMapper;

        public SupplierRepositoryTests(MongoDbFixture fixture)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SupplierModel, SupplierDataModel>().ReverseMap();
            });
            _mapper = configuration.CreateMapper();

            _customMapper = new Infra.Mapper.Mapper(_mapper);

            _repository = new SupplierRepository(fixture.Database, _customMapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddSupplier()
        {
            var supplier = new SupplierModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Supplier1",
                Address = "123 Main St",
                ContactEmail = "supplier1@example.com",
                PhoneNumber = "123456789",
                isActive = true
            };

            var result = await _repository.CreateAsync(supplier);

            Assert.NotNull(result);
            Assert.Equal(supplier.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSupplier_WhenSupplierExists()
        {
            var supplier = new SupplierModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Supplier1",
                Address = "123 Main St",
                ContactEmail = "supplier1@example.com",
                PhoneNumber = "123456789",
                isActive = true
            };
            await _repository.CreateAsync(supplier);

            var result = await _repository.GetByIdAsync(supplier.Id);

            Assert.NotNull(result);
            Assert.Equal(supplier.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateSupplier()
        {
            var supplier = new SupplierModel
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                Name = "Supplier1",
                Address = "123 Main St",
                ContactEmail = "supplier1@example.com",
                PhoneNumber = "123456789",
                isActive = true
            };
            await _repository.CreateAsync(supplier);

            var updatedSupplier = new SupplierModel
            {
                Id = supplier.Id,
                Name = "UpdatedSupplier",
                Address = "456 Another St",
                ContactEmail = "updatedsupplier@example.com",
                PhoneNumber = "987654321",
                isActive = true
            };

            await _repository.UpdateAsync(updatedSupplier);

            var result = await _repository.GetByIdAsync(updatedSupplier.Id);
            Assert.Equal("UpdatedSupplier", result.Name);
            Assert.Equal("456 Another St", result.Address);
            Assert.Equal("updatedsupplier@example.com", result.ContactEmail);
            Assert.Equal("987654321", result.PhoneNumber);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSetSupplierAsInactive()
        {
            var supplier = new SupplierModel
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                Name = "Supplier1",
                Address = "123 Main St",
                ContactEmail = "supplier1@example.com",
                PhoneNumber = "123456789",
                isActive = true
            };
            await _repository.CreateAsync(supplier);

            await _repository.DeleteAsync(supplier.Id);

            var result = await _repository.GetByIdAsync(supplier.Id);
            Assert.Null(result); 
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnTrue_WhenSupplierExists()
        {
            var supplier = new SupplierModel
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                Name = "Supplier1",
                Address = "123 Main St",
                ContactEmail = "supplier1@example.com",
                PhoneNumber = "123456789",
                isActive = true
            };
            await _repository.CreateAsync(supplier);

            var result = await _repository.ExistsAsync(supplier.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_ShouldReturnFalse_WhenSupplierDoesNotExist()
        {
            var result = await _repository.ExistsAsync(ObjectId.GenerateNewId().ToString());

            Assert.False(result);
        }
    }
}
