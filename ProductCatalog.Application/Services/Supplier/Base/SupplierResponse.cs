
namespace ProductCatalog.Application.Services.Supplier.Base
{
    public class SupplierResponse
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string ContactEmail { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; set; }

    }
}
