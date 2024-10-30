using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.UseCases.Product.Base;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Product;
using System.Net;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger,
            IProductService productService,
            IMapper mapper,
            Interfaces.IPresenter presenter) : base(presenter)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            await _productService.GetProducts(pageNumber, pageSize);
            return ResultForGet();
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            await _productService.GetById(id);
            return ResultForGet();
        }

        [HttpPost(Name = "Insert")]
        public async Task<IActionResult> Insert(CreateProductRequest request)
        {
            await _productService.Create(request);
            return ResultForPost(HttpStatusCode.Created);
        }

        [HttpPut(Name = "Update")]
        public async Task<IActionResult> Update(UpdateProductRequest request)
        {
            await _productService.Update(request);
            return ResultForPut(HttpStatusCode.OK);
        }

        [HttpDelete("{id}", Name = "DeleteById")]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _productService.Disable(id);
            return ResultForPut(HttpStatusCode.OK);
        }
    }
}