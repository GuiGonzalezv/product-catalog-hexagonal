using ProductCatalog.Domain.Dtos.Product;

namespace ProductCatalog.Application.UseCases.Product.Base
{
    public class CreateProductRequest : ICreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierId { get; set; }
    }
}
