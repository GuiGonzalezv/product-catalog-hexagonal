﻿
namespace ProductCatalog.Domain.Dtos.Supplier
{
    public interface ISupplierResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}