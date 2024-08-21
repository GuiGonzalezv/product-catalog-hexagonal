namespace ProductCatalog.Application.Services.Product.Base
{
    public class CreateProductRequest 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierId { get; set; }

    }
}
