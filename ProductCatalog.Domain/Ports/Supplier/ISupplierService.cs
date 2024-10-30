using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Ports.Supplier
{
    public interface ISupplierService
    {
        Task GetSuppliersAsync();
        Task CreateSupplierAsync(SupplierModel supplier);
        Task GetSupplierByIdAsync(string id);
        Task UpdateSupplierAsync(SupplierModel supplier);
        Task DeleteSupplierAsync(string id);
    }
}
