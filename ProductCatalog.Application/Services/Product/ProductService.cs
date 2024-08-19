using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Product;
using ProductCatalog.Domain.Ports.Supplier;

namespace ProductCatalog.Application.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductService(IProductRepository repository, ISupplierRepository supplierRepository)
        {
            _repository = repository;
            _supplierRepository = supplierRepository;
        }

        public async Task<ProductModel> Create(ProductModel product)
        {
            await VerifySupplierExist(product.SupplierId);
            return await _repository.AddAsync(product);
        }

        public async Task<ProductModel> GetById(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            return await _repository.GetAllAsync();
        }

        public async Task Update(ProductModel product)
        {
            await VerifyProductExist(product.Id);
            await VerifySupplierExist(product.SupplierId);
            await _repository.UpdateAsync(product);
        }

        public async Task Disable(string id)
        {
            await VerifyProductExist(id);

            await _repository.DeleteAsync(id);
        }

        public async Task VerifySupplierExist(string id)
        {
            if (!await _supplierRepository.ExistsAsync(id))
            {
                throw new NotFoundException("O fornecedor não foi encontrado.");
            }
        }

        public async Task VerifyProductExist(string id)
        {
            if (!await _repository.ProductExist(id))
            {
                throw new NotFoundException("O produto com o Id especificado não existe.");
            }
        }
    }
}
