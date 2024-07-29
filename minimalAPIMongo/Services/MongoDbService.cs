﻿using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armazena a configuracao da aplicacao
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma referencia ao MongoDb
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a config da aplicacao como parametro
        /// </summary>
        /// <param name="configuration"></param>
        public MongoDbService(IConfiguration configuration)
        {
            //atribui a config recebida em _configuration
            _configuration = configuration;

            //obtem a string de conexao atraves do _configuration
            var connectonString = _configuration.GetConnectionString("DbConnection");

            //Cria um objeto MongoUrl que recebe como parametro a string de conexao
            var mongoUrl = MongoUrl.Create(connectonString);

            //Cria um client MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);
            
            //Obtem a referencia ao bd com o nome especificado na string de conexao
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
                                              //mongoUrl= mongodb://localhost:27017/
        }

        /// <summary>
        /// Propriedade para acessar o banco de dados
        /// Retorna a referencia ao bd
        /// </summary>
        public IMongoDatabase GetDatabase => _database;
    }
}
