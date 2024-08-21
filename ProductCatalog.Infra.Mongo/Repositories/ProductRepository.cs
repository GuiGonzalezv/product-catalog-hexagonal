using MongoDB.Driver;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Infra.Mongo.DataModel;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<ProductDataModel> _products;
    private readonly IMapper _mapper;
    private const string Entity = "products";

    public ProductRepository(IMongoDatabase database, IMapper mapper)
    {
        _products = database.GetCollection<ProductDataModel>(Entity);
        _mapper = mapper;
    }

    public async Task<(IEnumerable<ProductModel>, int TotalCount)> GetAllAsync(int? pageNumber, int? pageSize)
    {
        int skip = 0;
        int limit = int.MaxValue;

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            skip = (pageNumber.Value - 1) * pageSize.Value;
            limit = pageSize.Value;
        }

        var productsQuery = _products.Find(product => product.isActive);

        var totalCount = await productsQuery.CountDocumentsAsync();

        var products = await productsQuery
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<ProductModel>>(products);

        return (result, (int)totalCount);
    }

    public async Task<ProductModel> GetByIdAsync(string id)
    {
        var product = await _products.Find(product => product.Id == id && product.isActive).FirstOrDefaultAsync();
        return _mapper.Map<ProductModel>(product);
    }

    public async Task<ProductModel> AddAsync(ProductModel product)
    {
        var dataModel = _mapper.Map<ProductDataModel>(product);
        await _products.InsertOneAsync(dataModel);
        return _mapper.Map<ProductModel>(dataModel);
    }

    public async Task UpdateAsync(ProductModel product)
    {
        var updateDefinition = Builders<ProductDataModel>.Update
               .Set(s => s.Name, product.Name)
               .Set(s => s.SupplierId, product.SupplierId)
               .Set(s => s.StockQuantity, product.StockQuantity)
               .Set(s => s.Price, product.Price)
               .Set(s => s.Description, product.Description)
               .Set(s => s.UpdatedAt, DateTime.UtcNow);

        await _products.UpdateOneAsync(s => s.Id == product.Id, updateDefinition);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<ProductDataModel>.Filter.Eq(p => p.Id, id);

        var update = Builders<ProductDataModel>.Update.Set(p => p.isActive, false);

        await _products.UpdateOneAsync(filter, update);
    }

    public async Task<bool> ProductExist(string id)
    {
        var product = await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        return product is not null;

    }
}
