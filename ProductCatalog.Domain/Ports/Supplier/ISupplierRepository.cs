using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Ports.Supplier
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierModel>> GetAllAsync();
        Task<SupplierModel> CreateAsync(SupplierModel supplier);
        Task<SupplierModel> GetByIdAsync(string id);
        Task UpdateAsync(SupplierModel supplier);
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);

    }
}
