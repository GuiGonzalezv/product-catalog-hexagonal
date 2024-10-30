using ProductCatalog.Domain.Dtos.Supplier;

namespace ProductCatalog.Application.UseCases.Supplier.Base
{ 
    public class CreateSupplierRequest : ICreateSupplierRequest
    {
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
