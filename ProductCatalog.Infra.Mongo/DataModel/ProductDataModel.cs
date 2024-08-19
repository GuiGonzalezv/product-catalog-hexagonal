

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProductCatalog.Infra.Mongo.DataModel
{
    public class ProductDataModel
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("stock_quantity")]
        public int StockQuantity { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("supplier_id")]
        public string SupplierId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("is_active")]
        public bool isActive { get; set; }
    }
}
