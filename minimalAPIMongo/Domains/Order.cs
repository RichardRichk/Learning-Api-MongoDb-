using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        [BsonId]

        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Date")]
        public DateTime? OrderDate { get; set; }

        [BsonElement("Status")]
        public string? Status { get; set; }

        [BsonElement("UserId")]
        public string? UserId { get; set; }

        [BsonElement("ProductId")]
        public string? ProductId { get; set; }


        public Dictionary<string, string> AdditionalAttributes { get; set; }


        public Order() 
        { 
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
