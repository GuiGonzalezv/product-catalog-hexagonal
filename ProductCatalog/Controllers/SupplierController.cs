using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Domain.Ports;
using System.Net;
using ProductCatalog.Application.UseCases.Supplier.Base;


namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("/api/suppliers")]
    public class SupplierController : BaseController
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(ILogger<SupplierController> logger,
            ISupplierService supplierService,
            IMapper mapper,
            Interfaces.IPresenter presenter) : base(presenter)
        {
            _logger = logger;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetSuppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            await _supplierService.GetSuppliersAsync();
            return ResultForGet();
        }

        [HttpGet("{id}", Name = "GetSupplierById")]
        public async Task<IActionResult> GetSupplierById(string id)
        {
            await _supplierService.GetSupplierByIdAsync(id);
            return ResultForGet();
        }

        [HttpPost(Name = "InsertSupplier")]
        public async Task<IActionResult> InsertSupplier(CreateSupplierRequest request)
        {
            var supplier = _mapper.Map<SupplierModel>(request);
            await _supplierService.CreateSupplierAsync(supplier);
            return ResultForPost(HttpStatusCode.Created);
        }

        [HttpPut(Name = "UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierRequest request)
        {
            var supplier = _mapper.Map<SupplierModel>(request);
            await _supplierService.UpdateSupplierAsync(supplier);
            return ResultForPut(HttpStatusCode.OK);
        }

        [HttpDelete("{id}", Name = "DeleteSupplierById")]
        public async Task<IActionResult> DeleteSupplierById(string id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return ResultForDelete(HttpStatusCode.OK);
        }
    }
}
