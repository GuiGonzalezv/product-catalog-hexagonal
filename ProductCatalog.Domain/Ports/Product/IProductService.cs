using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Ports.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProducts();

        Task<ProductModel> GetById(string id);

        Task<ProductModel> Create(ProductModel product);

        Task Update(ProductModel product);

        Task Disable(string id);
    }
}
