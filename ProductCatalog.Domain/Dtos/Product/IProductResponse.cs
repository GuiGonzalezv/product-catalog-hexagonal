namespace ProductCatalog.Domain.Dtos.Product
{
    public interface IProductResponse
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierId { get; set; }
    }
}
