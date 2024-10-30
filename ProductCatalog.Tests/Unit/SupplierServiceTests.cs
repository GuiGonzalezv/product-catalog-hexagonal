using Moq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Domain.UseCases;
using ProductCatalog.Domain.UseCases.Supplier;
using ProductCatalog.Interfaces;
using Xunit;

public class SupplierServiceTests
{
    private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
    private readonly Mock<IPresenter> _outputPortMock;
    private readonly Mock<IMapper> _mapper;
    private readonly SupplierService _supplierService;

    public SupplierServiceTests()
    {
        _supplierRepositoryMock = new Mock<ISupplierRepository>();
        _outputPortMock = new Mock<IPresenter>();
        _mapper = new Mock<IMapper>();
        _supplierService = new SupplierService(_supplierRepositoryMock.Object, _mapper.Object, _outputPortMock.Object);
    }

    [Fact]
    public async Task CreateSupplierAsync_ShouldReturnCreatedSupplier()
    {
        // Arrange
        var supplier = new SupplierModel { Id = "1" };
        _supplierRepositoryMock.Setup(repo => repo.CreateAsync(supplier)).ReturnsAsync(supplier);

        // Act
        await _supplierService.CreateSupplierAsync(supplier);

        // Assert
        var response = new UseCaseOutput<SupplierModel>(supplier);

        _supplierRepositoryMock.Verify(repo => repo.CreateAsync(supplier), Times.Once);

        _outputPortMock.Verify(p => p.Handle(It.Is<UseCaseOutput<SupplierModel>>(output =>
             output.HasErrors == false && output.Data == supplier)),
             Times.Once);
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
        await _supplierService.GetSuppliersAsync();

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        _outputPortMock.Verify(p => p.Handle(It.Is<UseCaseOutput<IEnumerable<SupplierModel>>>(output =>
                     output.HasErrors == false && output.Data == suppliers)),
                     Times.Once);
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
        await _supplierService.GetSupplierByIdAsync(supplierId);

        // Assert
        _supplierRepositoryMock.Verify(repo => repo.GetByIdAsync(supplierId), Times.Once);

        _outputPortMock.Verify(p => p.Handle(It.Is<UseCaseOutput<SupplierModel>>(output =>
                     output.HasErrors == false && output.Data == supplier)),
                     Times.Once);
    }

    [Fact]
    public async Task GetSupplierByIdAsync_ShouldHandleError_WhenSupplierDoesNotExist()
    {
        // Arrange
        var supplierId = "1";
        var supplier = new SupplierModel { Id = supplierId };

        _supplierRepositoryMock.Setup(repo => repo.GetByIdAsync(supplierId)).ReturnsAsync(supplier);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(false);

        // Act & Assert
        await _supplierService.GetSupplierByIdAsync(supplierId);

        _outputPortMock.Verify(p => p.Handle(It.Is<UseCaseOutput<ErrorResponse>>(output =>
             output.HasErrors == true &&
             output.Errors.Any(error => error.Message == "O fornecedor não foi encontrado."))),
             Times.Once);
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
    public async Task VerifySupplierExist_ShouldReturnFalse_WhenSupplierDoesNotExist()
    {
        var supplierId = "invalidSupplier";
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(false);

        var response = await _supplierService.VerifySupplierExist(supplierId);

        _outputPortMock.Verify(p => p.Handle(It.Is<UseCaseOutput<ErrorResponse>>(output =>
             output.HasErrors == true &&
             output.Errors.Any(error => error.Message == "O fornecedor não foi encontrado."))),
             Times.Once);
    }
}
