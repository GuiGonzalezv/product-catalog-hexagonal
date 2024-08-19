using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Services.Supplier.Base;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports.Supplier;
using AutoMapper;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("/api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService, IMapper mapper)
        {
            _logger = logger;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetSuppliers")]
        public async Task<IEnumerable<SupplierResponse>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetSuppliersAsync();
            return _mapper.Map<IEnumerable<SupplierResponse>>(suppliers);
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public async Task<SupplierResponse> GetSupplierById(string id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            return _mapper.Map<SupplierResponse>(supplier);
        }

        [HttpPost(Name = "InsertSupplier")]
        public async Task<SupplierResponse> InsertSupplier(CreateSupplierRequest request)
        {
            var supplier = _mapper.Map<SupplierModel>(request);
            var response = await _supplierService.CreateSupplierAsync(supplier);
            return _mapper.Map<SupplierResponse>(response);
        }

        [HttpPut(Name = "UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierRequest request)
        {
            var supplier = _mapper.Map<SupplierModel>(request);
            await _supplierService.UpdateSupplierAsync(supplier);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteSupplierById")]
        public async Task<IActionResult> DeleteSupplierById(string id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Ok();
        }
    }
}
