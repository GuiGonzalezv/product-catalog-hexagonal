using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports.Supplier;


namespace ProductCatalog.Application.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<SupplierModel> CreateSupplierAsync(SupplierModel supplier)
        {
            var response = await _supplierRepository.CreateAsync(supplier);
            return response;
        }

        public async Task<IEnumerable<SupplierModel>> GetSuppliersAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<SupplierModel> GetSupplierByIdAsync(string id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            await VerifySupplierExist(supplier.Id);
            return supplier;
        }

        public async Task UpdateSupplierAsync(SupplierModel supplierModel)
        {
            var supplier = await _supplierRepository.GetByIdAsync(supplierModel.Id);
            await VerifySupplierExist(supplier.Id);
            await _supplierRepository.UpdateAsync(supplierModel);
        }

        public async Task DeleteSupplierAsync(string id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            await VerifySupplierExist(supplier.Id);
            await _supplierRepository.DeleteAsync(id);
        }

        public async Task VerifySupplierExist(string id)
        {
            if (!await _supplierRepository.ExistsAsync(id))
            {
                throw new NotFoundException("O fornecedor não foi encontrado.");
            }
        }
    }

}
