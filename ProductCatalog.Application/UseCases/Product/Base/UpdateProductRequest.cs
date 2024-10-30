using ProductCatalog.Domain.Dtos.Product;

namespace ProductCatalog.Application.UseCases.Product.Base
{
    public class UpdateProductRequest : CreateProductRequest, IUpdateProductRequest
    {
        public string Id { get; set; }
    }
}
