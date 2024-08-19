namespace ProductCatalog.Domain.Entities
{
    public class SupplierModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool isActive { get; set; }
    }
}
