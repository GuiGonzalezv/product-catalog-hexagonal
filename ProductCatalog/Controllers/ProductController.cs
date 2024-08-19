using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Services.Product.Base;
using ProductCatalog.Application.Services.Product.Validations;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Product;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet(Name = "Get")]
        public async Task<IEnumerable<ProductResponse>> Get()
        {
            var products = await _productService.GetProducts();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ProductResponse> GetById(string id)
        {
            var product = await _productService.GetById(id);
            return _mapper.Map<ProductResponse>(product);
        }

        [HttpPost(Name = "Insert")]
        public async Task<ProductResponse> Insert(CreateProductRequest request)
        {
            var product = _mapper.Map<ProductModel>(request);
            var response = await _productService.Create(product);
            return _mapper.Map<ProductResponse>(response);

        }

        [HttpPut(Name = "Update")]
        public async Task<OkResult> Update(UpdateProductRequest request)
        {
            var product = _mapper.Map<ProductModel>(request);
            await _productService.Update(product);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteById")]
        public async Task<OkResult> DeleteById(string id)
        {
            await _productService.Disable(id);
            return Ok();
        }
    }
}