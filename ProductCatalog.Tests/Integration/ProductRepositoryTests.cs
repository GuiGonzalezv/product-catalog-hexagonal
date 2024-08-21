using AutoMapper;
using MongoDB.Bson; 
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infra.Mongo.DataModel;
using Xunit;

namespace ProductCatalog.Tests.Integration
{
    public class ProductRepositoryTests : IClassFixture<MongoDbFixture>
    {
        private readonly ProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly Domain.Ports.IMapper _customMapper;

        public ProductRepositoryTests(MongoDbFixture fixture)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductModel, ProductDataModel>().ReverseMap();
            });

            _mapper = configuration.CreateMapper();

            _customMapper = new Infra.Mapper.Mapper(_mapper);

            _repository = new ProductRepository(fixture.Database, _customMapper);
        }

        [Fact]
        public async Task AddAsync_ShouldAddProduct()
        {
            var product = new ProductModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Product1",
                SupplierId = ObjectId.GenerateNewId().ToString(),
                StockQuantity = 10,
                Price = 100,
                Description = "Test Product",
                isActive = true
            };

            var result = await _repository.AddAsync(product);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            var product = new ProductModel
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                Name = "Product1",
                SupplierId = ObjectId.GenerateNewId().ToString(),
                StockQuantity = 10,
                Price = 100,
                Description = "Test Product",
                isActive = true
            };
            await _repository.AddAsync(product);

            var result = await _repository.GetByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {
            var product = new ProductModel
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                Name = "Product1",
                SupplierId = ObjectId.GenerateNewId().ToString(),
                StockQuantity = 10,
                Price = 100,
                Description = "Test Product",
                isActive = true
            };
            await _repository.AddAsync(product);

            var updatedProduct = new ProductModel
            {
                Id = product.Id, 
                Name = "UpdatedProduct",
                SupplierId = ObjectId.GenerateNewId().ToString(),
                StockQuantity = 20,
                Price = 150,
                Description = "Updated Description",
                isActive = true
            };

            await _repository.UpdateAsync(updatedProduct);

            var result = await _repository.GetByIdAsync(updatedProduct.Id);
            Assert.Equal("UpdatedProduct", result.Name);
            Assert.Equal(20, result.StockQuantity);
            Assert.Equal(150, result.Price);
            Assert.Equal("Updated Description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSetProductAsInactive()
        {
            var product = new ProductModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Product1",
                SupplierId = ObjectId.GenerateNewId().ToString(),
                StockQuantity = 10,
                Price = 100,
                Description = "Test Product",
                isActive = true
            };
            await _repository.AddAsync(product);

            await _repository.DeleteAsync(product.Id);

            var result = await _repository.GetByIdAsync(product.Id);
            Assert.Null(result); 
        }
    }
}
