using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Product;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Domain.Ports.Validation;
using ProductCatalog.Application.UseCases.Product.Base;
using ProductCatalog.Application.UseCases.Shared;
using ProductCatalog.Domain.Dtos.Product;

namespace ProductCatalog.Application.UseCases.Product
{
    public class ProductService : UseCaseBase, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IValidator<ICreateProductRequest> _validator;
        private readonly IValidator<IUpdateProductRequest> _updateValidator;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository,
            ISupplierRepository supplierRepository,
            IValidator<ICreateProductRequest> validator,
            IValidator<IUpdateProductRequest> updateValidator,
            IMapper mapper,
            IOutputPort outputPort) : base(outputPort, mapper)
        {
            _repository = repository;
            _supplierRepository = supplierRepository;
            _updateValidator = updateValidator;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task Create(ICreateProductRequest request)
        {
            if (!IsValid(_validator, request)) return;

            var product = _mapper.Map<ProductModel>(request);
            
            if(!await VerifySupplierExist(product.SupplierId))
            {
                Handle<ErrorResponse>("Fornecedor não encontrado.");
                return;
            }

            var response = await _repository.AddAsync(product);

            Handle(response);
        }

        public async Task GetById(string id)
        {
            var response =  await _repository.GetByIdAsync(id);
            Handle(response);
        }

        public async Task GetProducts(int? pageNumber, int? pageSize)
        {
            var (products, totalCount) = await _repository.GetAllAsync(pageNumber, pageSize);

            var productResponse = _mapper.Map<IEnumerable<ProductResponse>>(products);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                var response = new PageResponse<ProductResponse>(productResponse, totalCount, pageNumber.Value, pageSize.Value);
                Handle(response);
                return;
            }

            Handle<IEnumerable<ProductModel>>(productResponse);
        }   

        public async Task Update(IUpdateProductRequest request)
        {
            if (!IsValid(_updateValidator, request)) return;

            var product = _mapper.Map<ProductModel>(request);
            
            if (!await VerifyProductExist(product.Id))
            {
                Handle<ErrorResponse>("Produto não encontrado");
                return;
            }

            if (!await VerifySupplierExist(product.SupplierId))
            {
                Handle<ErrorResponse>("Fornecedor não encontrado");
                return;
            }
            
            await _repository.UpdateAsync(product);
            Handle(product);
        }

        public async Task Disable(string id)
        {
            if(!await VerifyProductExist(id))
            {
                Handle<ErrorResponse>("Produto não encontrado");
                return;
            }

            await _repository.DeleteAsync(id);
        }

        public async Task<bool> VerifySupplierExist(string id)
        {
            return await _supplierRepository.ExistsAsync(id);
        }

        public async Task<bool> VerifyProductExist(string id)
        {
            return await _repository.ProductExist(id);
        }
    }
}
