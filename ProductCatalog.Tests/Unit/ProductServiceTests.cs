using Moq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Application.Services.Product;
using Xunit;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _supplierRepositoryMock = new Mock<ISupplierRepository>();
        _productService = new ProductService(_productRepositoryMock.Object, _supplierRepositoryMock.Object);
    }

    [Fact]
    public async Task Create_ShouldAddProduct_WhenSupplierExists()
    {
        var product = new ProductModel { Id = "1", SupplierId = "supplier1" };
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(product.SupplierId)).ReturnsAsync(true);
        _productRepositoryMock.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product);

        var result = await _productService.Create(product);

        _productRepositoryMock.Verify(repo => repo.AddAsync(product), Times.Once);
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task Create_ShouldThrowNotFoundException_WhenSupplierDoesNotExist()
    {
        var product = new ProductModel { SupplierId = "invalidSupplier" };
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(product.SupplierId)).ReturnsAsync(false);
        
        await Assert.ThrowsAsync<NotFoundException>(() => _productService.Create(product));
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_WhenProductExists()
    {
        var productId = "1";
        var product = new ProductModel { Id = productId };
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

        var result = await _productService.GetById(productId);

        _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task Update_ShouldUpdateProduct_WhenProductAndSupplierExist()
    {
        var product = new ProductModel { Id = "1", SupplierId = "supplier1" };
        _productRepositoryMock.Setup(repo => repo.ProductExist(product.Id)).ReturnsAsync(true);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(product.SupplierId)).ReturnsAsync(true);

        await _productService.Update(product);

        _productRepositoryMock.Verify(repo => repo.UpdateAsync(product), Times.Once);
    }

    [Fact]
    public async Task Disable_ShouldDeleteProduct_WhenProductExists()
    {
        var productId = "1";
        _productRepositoryMock.Setup(repo => repo.ProductExist(productId)).ReturnsAsync(true);

        await _productService.Disable(productId);

        _productRepositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
    }

    [Fact]
    public async Task VerifySupplierExist_ShouldThrowNotFoundException_WhenSupplierDoesNotExist()
    {
        var supplierId = "invalidSupplier";
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _productService.VerifySupplierExist(supplierId));
    }

    [Fact]
    public async Task VerifyProductExist_ShouldThrowNotFoundException_WhenProductDoesNotExist()
    {
        var productId = "invalidProduct";
        _productRepositoryMock.Setup(repo => repo.ProductExist(productId)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _productService.VerifyProductExist(productId));
    }
}
