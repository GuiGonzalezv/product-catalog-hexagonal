using Moq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Supplier;
using Xunit;
using ProductCatalog.Domain.Dtos;
using ProductCatalog.Domain.Ports.Validation;
using ProductCatalog.Application.UseCases;
using ProductCatalog.Interfaces;
using ProductCatalog.Application.UseCases.Product;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ISupplierRepository> _supplierRepositoryMock;
    private readonly Mock<IPresenter> _outputPort;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IValidator<CreateProductRequest>> _createProductValidator;
    private readonly Mock<IValidator<UpdateProductRequest>> _updateProductValidator;

    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _supplierRepositoryMock = new Mock<ISupplierRepository>();
        _outputPort = new Mock<IPresenter>();
        _mapper = new Mock<IMapper>();
        _createProductValidator = new Mock<IValidator<CreateProductRequest>>();
        _updateProductValidator = new Mock<IValidator<UpdateProductRequest>>();
        _productService = new ProductService(_productRepositoryMock.Object, _supplierRepositoryMock.Object, _createProductValidator.Object , _updateProductValidator.Object, _mapper.Object, _outputPort.Object);
    }

    [Fact]
    public async Task Create_ShouldAddProduct_WhenSupplierExists()
    {
        var productRequest = new CreateProductRequest { SupplierId = "supplier1", Description = "x", Name = "x", Price = 10, StockQuantity = 10 };

        var product = new ProductModel { Id = "1", SupplierId = "supplier1", Description = "x", Name = "x", Price = 10, StockQuantity = 10 };

        _createProductValidator.Setup(v => v.Validate(productRequest)).Returns(new ValidationResult(true));

        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(product.SupplierId)).ReturnsAsync(true);

        _productRepositoryMock.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product);

        _mapper.Setup(map => map.Map<ProductModel>(productRequest)).Returns(product);

        await _productService.Create(productRequest);

        var response = new UseCaseOutput<ProductModel>(product);

        _productRepositoryMock.Verify(repo => repo.AddAsync(product), Times.Once);

        _outputPort.Verify(p => p.Handle(It.Is<UseCaseOutput<ProductModel>>(output =>
            output.HasErrors == false && output.Data == product)),
            Times.Once);
    }

    [Fact]
    public async Task Create_ShouldInvokePresenterHandle_WhenSupplierDoesNotExist()
    {
        var product = new CreateProductRequest { SupplierId = "invalidSupplier" };

        _supplierRepositoryMock
            .Setup(repo => repo.ExistsAsync(product.SupplierId))
            .ReturnsAsync(false);

        var errors = new List<ValidationResultErrors>();
        errors.Add(new ValidationResultErrors { ErrorMessage = "O Id inserido para o fornecedor é invalido.", PropertyName = "Id" });
        _createProductValidator.Setup(validator => validator.Validate(product)).Returns(new ValidationResult(false, errors));

        await _productService.Create(product);

        _outputPort.Verify(p => p.Handle(It.Is<UseCaseOutput<ErrorResponse>>(output =>
            output.HasErrors == true &&
            output.Errors.Any(error => error.Message == "O Id inserido para o fornecedor é invalido."))),
            Times.Once);
    }

    [Fact]
    public async Task GetById_ShouldReturnProduct_WhenProductExists()
    {
        var productId = "1";
        var product = new ProductModel { Id = productId };
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);

        await _productService.GetById(productId);

        _productRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldUpdateProduct_WhenProductAndSupplierExist()
    {
        var product = new ProductModel { Id = "1", SupplierId = "supplier1" };
        var productRequest = new UpdateProductRequest { Id = "1", SupplierId = "supplier1" };

        _productRepositoryMock.Setup(repo => repo.ProductExist(product.Id)).ReturnsAsync(true);
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(product.SupplierId)).ReturnsAsync(true);
        _mapper.Setup(map => map.Map<ProductModel>(productRequest)).Returns(product);
        _updateProductValidator.Setup(v => v.Validate(productRequest)).Returns(new ValidationResult(true));

        await _productService.Update(productRequest);

        _productRepositoryMock.Verify(repo => repo.UpdateAsync(product), Times.Once);

        _outputPort.Verify(p => p.Handle(It.Is<UseCaseOutput<ProductModel>>(output =>
            output.HasErrors == false && output.Data == product)),
            Times.Once);
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
    public async Task VerifySupplierExist_ShouldReturnFalse_WhenSupplierDoesNotExist()
    {
        var supplierId = "invalidSupplier";
        _supplierRepositoryMock.Setup(repo => repo.ExistsAsync(supplierId)).ReturnsAsync(false);

        var result = await _productService.VerifySupplierExist(supplierId);

        Assert.False(result);
    }

    [Fact]
    public async Task VerifyProductExist_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        var productId = "invalidProduct";
        _productRepositoryMock.Setup(repo => repo.ProductExist(productId)).ReturnsAsync(false);

        var result = await _productService.VerifyProductExist(productId);

        Assert.False(result);
    }
}
