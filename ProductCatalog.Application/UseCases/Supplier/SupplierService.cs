using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Supplier;

namespace ProductCatalog.Application.UseCases.Supplier
{
    public class SupplierService : UseCaseBase, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        private readonly IOutputPort _outputPort;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IMapper mapper,
            IOutputPort outputPort) : base(outputPort, mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _outputPort = outputPort;
        }

        public async Task CreateSupplierAsync(SupplierModel supplier)
        {
            var response = await _supplierRepository.CreateAsync(supplier);
            Handle(response);
        }

        public async Task GetSuppliersAsync()
        {
            var response = await _supplierRepository.GetAllAsync();
            Handle(response);
        }

        public async Task GetSupplierByIdAsync(string id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (!await VerifySupplierExist(supplier.Id)) return;
            Handle(supplier);
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

        public async Task<bool> VerifySupplierExist(string id)
        {
            if (!await _supplierRepository.ExistsAsync(id))
            {
                Handle<ErrorResponse>("O fornecedor não foi encontrado.");
                return false;
            }
            return true;
        }
    }

}
