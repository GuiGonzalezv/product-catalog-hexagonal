using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProductCatalog.Infra.Mongo.DataModel
{
    public class SupplierDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("contact_email")]
        public string ContactEmail { get; set; }

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

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
