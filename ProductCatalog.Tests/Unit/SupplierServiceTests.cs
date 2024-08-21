using Moq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Application.Services.Supplier;
using Xunit;

public class SupplierServiceTests
{
    private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
    private readonly SupplierService _supplierService;

    public SupplierServiceTests()
    {
        _supplierRepositoryMock = new Mock<ISupplierRepository>();
        _supplierService = new SupplierService(_supplierRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateSupplierAsync_ShouldReturnCreatedSupplier()
    {
        // Arrange
        var supplier = new SupplierModel { Id = "1" };
        _supplierRepositoryMock.Setup(repo => repo.CreateAsync(supplier)).ReturnsAsync(supplier);

        // Act
        var result = await _supplierService.CreateSupplierAsync(supplier);

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.CreateAsync(supplier), Times.Once);
        Assert.Equal(supplier, result);
    }

    [Fact]
    public async Task GetSuppliersAsync_ShouldReturnListOfSuppliers()
    {
        // Arrange
        var suppliers = new List<SupplierModel>
        {
            new SupplierModel { Id = "1" },
            new SupplierModel { Id = "2" }
        };
        _supplierRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(suppliers);

        // Act
        var result = await _supplierService.GetSuppliersAsync();

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        Assert.Equal(suppliers, result);
    }

    [Fact]
    public async Task GetSupplierByIdAsync_ShouldReturnSupplier_WhenSupplierExists()
    {
        // Arrange
        var supplierId = "1";
        var supplier = new SupplierModel { Id = supplierId };
        _supplierRepositoryMock.Setup(repo => repo.GetByIdAsync(supplierId)).ReturnsAsync(supplier);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(true);

        // Act
        var result = await _supplierService.GetSupplierByIdAsync(supplierId);

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.GetByIdAsync(supplierId), Times.Once);
        Assert.Equal(supplier, result);
    }

    [Fact]
    public async Task GetSupplierByIdAsync_ShouldThrowNotFoundException_WhenSupplierDoesNotExist()
    {
        // Arrange
        var supplierId = "1";
        var supplier = new SupplierModel { Id = supplierId };
        _supplierRepositoryMock.Setup(repo => repo.GetByIdAsync(supplierId)).ReturnsAsync(supplier);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _supplierService.GetSupplierByIdAsync(supplierId));
    }

    [Fact]
    public async Task UpdateSupplierAsync_ShouldUpdateSupplier_WhenSupplierExists()
    {
        // Arrange
        var supplier = new SupplierModel { Id = "1" };
        _supplierRepositoryMock.Setup(repo => repo.GetByIdAsync(supplier.Id)).ReturnsAsync(supplier);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplier.Id)).ReturnsAsync(true);

        // Act
        await _supplierService.UpdateSupplierAsync(supplier);

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.UpdateAsync(supplier), Times.Once);
    }

    [Fact]
    public async Task DeleteSupplierAsync_ShouldDeleteSupplier_WhenSupplierExists()
    {
        // Arrange
        var supplierId = "1";
        var supplier = new SupplierModel { Id = supplierId };
        _supplierRepositoryMock.Setup(repo => repo.GetByIdAsync(supplierId)).ReturnsAsync(supplier);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(true);

        // Act
        await _supplierService.DeleteSupplierAsync(supplierId);

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.DeleteAsync(supplierId), Times.Once);
    }

    [Fact]
    public async Task VerifySupplierExist_ShouldThrowNotFoundException_WhenSupplierDoesNotExist()
    {
        // Arrange
        var supplierId = "invalidSupplier";
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _supplierService.VerifySupplierExist(supplierId));
    }
}
