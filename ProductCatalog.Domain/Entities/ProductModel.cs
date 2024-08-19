namespace ProductCatalog.Domain.Entities
{
    public class ProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool isActive { get; set; }

        public void DecreaseStock(int quantity) 
        {
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Insufficient stock.");

            StockQuantity -= quantity;
        }
    }

}
