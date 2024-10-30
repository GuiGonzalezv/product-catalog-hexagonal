using ProductCatalog.Domain.Dtos.Product;

namespace ProductCatalog.Domain.Ports.Product
{
    public interface IProductService
    {
        Task GetProducts(int? pageNumber, int? pageSize);

        Task GetById(string id);

        Task Create(ICreateProductRequest request);

        Task Update(IUpdateProductRequest product);

        Task Disable(string id);
    }
}
