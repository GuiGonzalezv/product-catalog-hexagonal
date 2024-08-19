namespace ProductCatalog.Application.Services.Supplier.Base
{
    public class CreateSupplierRequest
    {
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
