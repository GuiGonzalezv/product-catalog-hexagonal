using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Ports.Supplier
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierModel>> GetSuppliersAsync();
        Task<SupplierModel> CreateSupplierAsync(SupplierModel supplier);
        Task<SupplierModel> GetSupplierByIdAsync(string id);
        Task UpdateSupplierAsync(SupplierModel supplier);
        Task DeleteSupplierAsync(string id);
    }
}
