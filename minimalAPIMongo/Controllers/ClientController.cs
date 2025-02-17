﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<User> _users;

        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _users = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var clients = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();

                foreach (var client in clients)
                {
                    client.User = await _users.Find(_ => true).FirstOrDefaultAsync();
                }

                return Ok(clients);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create(Client client)
        {
            try
            {
                await _client.InsertOneAsync(client);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Atualizar(Client client)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, client.Id);

                await _client.ReplaceOneAsync(filter, client);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(String id)
        {
            try
            {
                var client = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();
                client.User = await _users.Find(x => x.Id == client.UserId).FirstOrDefaultAsync();

                return client is not null ? Ok(client) : BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Client>> Delete(String id)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, id);

                await _client.DeleteOneAsync(filter);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
