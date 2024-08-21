using MongoDB.Driver;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Ports;
using ProductCatalog.Domain.Ports.Supplier;
using ProductCatalog.Infra.Mongo.DataModel;

namespace ProductCatalog.Infra.Mongo.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IMongoCollection<SupplierDataModel> _supplierCollection;
        private const string Entity = "suppliers";
        private readonly IMapper _mapper;


        public SupplierRepository(IMongoDatabase database, IMapper mapper)
        {
            _supplierCollection = database.GetCollection<SupplierDataModel>(Entity);
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierModel>> GetAllAsync()
        {
            var supplier = await _supplierCollection.Find(_ => _.isActive).ToListAsync();
            return _mapper.Map<IEnumerable<SupplierModel>>(supplier);
        }

        public async Task<SupplierModel> GetByIdAsync(string id)
        {
            var response = await _supplierCollection.Find(supplier => supplier.Id == id && supplier.isActive).FirstOrDefaultAsync();
            return _mapper.Map<SupplierModel>(response);
        }

        public async Task<SupplierModel> CreateAsync(SupplierModel supplier)
        {
            var dataModel = _mapper.Map<SupplierDataModel>(supplier);
            await _supplierCollection.InsertOneAsync(dataModel);
            return _mapper.Map<SupplierModel>(dataModel);
        }

        public async Task UpdateAsync(SupplierModel supplier)
        {
            var updateDefinition = Builders<SupplierDataModel>.Update
               .Set(s => s.Name, supplier.Name)
               .Set(s => s.Address, supplier.Address)
               .Set(s => s.ContactEmail, supplier.ContactEmail)
               .Set(s => s.PhoneNumber, supplier.PhoneNumber)
               .Set(s => s.UpdatedAt, DateTime.UtcNow);

            await _supplierCollection.UpdateOneAsync(s => s.Id == supplier.Id, updateDefinition);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<SupplierDataModel>.Filter.Eq(p => p.Id, id);

            var update = Builders<SupplierDataModel>.Update.Set(p => p.isActive, false);

            await _supplierCollection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var count = await _supplierCollection.CountDocumentsAsync(supplier => supplier.Id == id);
            return count > 0;
        }
    }
}
