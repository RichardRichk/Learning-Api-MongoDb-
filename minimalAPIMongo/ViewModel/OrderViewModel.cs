using minimalAPIMongo.Domains;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.ViewModel
{
    public class OrderViewModel
    {
        [BsonId]

        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("Date")]
        public DateTime? OrderDate { get; set; }

        [BsonElement("Status")]
        public string? Status { get; set; }

        [BsonElement("ClientId")]
        public string? ClientId { get; set; }

        [BsonIgnoreIfDefault]
        [JsonIgnore]
        public Client? Client { get; set; }

        [BsonElement("ProductId")]
        public List<string>? ProductId { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnore]
        [JsonIgnore]
        public List<Product>? Product { get; set; }

    }
}
