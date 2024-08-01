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
        public DateTime? OrderDate { get; set; }

        public string? Status { get; set; }

        public string? ClientId { get; set; }

        public List<string>? ProductId { get; set; }

        public List<Product>? Product { get; set; }
        public Dictionary<string, string>? AdditionalAttributes { get; set; }

    }
}
