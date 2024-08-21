using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Ports.Product
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductModel>, int TotalCount)> GetProducts(int? pageNumber, int? pageSize);

        Task<ProductModel> GetById(string id);

        Task<ProductModel> Create(ProductModel product);

        Task Update(ProductModel product);

        Task Disable(string id);
    }
}
