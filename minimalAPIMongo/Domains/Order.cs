﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace minimalAPIMongo.Domains
{
    public class Order
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
        [JsonIgnore]
        public string? ClientId { get; set; }

        [BsonIgnoreIfDefault]
        public Client? Client { get; set; }

        [BsonElement("ProductId")]
        [JsonIgnore]
        //Referencia para que eu consiga cadastrar um pedido com os produtos
        public List<string>? ProductId { get; set; }

        [BsonIgnoreIfDefault]
        //Referencia para que quando eu liste os pedidos, venha os dados de cada produto(lista)
        public List<Product>? Product { get; set; }


        public Dictionary<string, string> AdditionalAttributes { get; set; }


        public Order() 
        { 
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
