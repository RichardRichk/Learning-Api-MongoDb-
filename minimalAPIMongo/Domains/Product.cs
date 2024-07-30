using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class Product
    {
        //Define que esta prop e Id do objeto
        [BsonId] 

        //Define o nome do campo MongoDb como "_id" e o tipo como "ObjectId"
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        //Adiciona um dicionario para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes { get; set; }


        /// <summary>
        /// Ao ser instanciado um objeto da calsse Product, o atributo "AdditionalAttributes" ja vira com um novo dicionario e portanto habilitado para adicionar +(mais) atributos
        /// </summary>
        public Product() 
        { 
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
