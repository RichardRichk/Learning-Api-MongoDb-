using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using minimalAPIMongo.ViewModel;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<Product> _products;

        public OrderController(MongoDbService mongoDbService)
        {
            _orders = mongoDbService.GetDatabase.GetCollection<Order>("order");
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _products = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }


        [HttpGet]

        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var orders = await _orders.Find(FilterDefinition<Order>.Empty).ToListAsync();

                foreach (var item in orders)
                {
                    item.Client = await _client.Find(_ => true).FirstOrDefaultAsync();
                }

                return Ok(orders);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Order>> Create(OrderViewModel orderViewModel)
        {
            try
            {
                Order order = new Order();
                order.Id = orderViewModel.Id;
                order.OrderDate = orderViewModel.OrderDate;
                order.Status = orderViewModel.Status;
                order.ProductId = orderViewModel.ProductId;
                order.ClientId = orderViewModel.ClientId;

                var client = await _client.Find(x => x.Id == order.ClientId).FirstOrDefaultAsync();
                if (client == null)
                {
                    return NotFound();
                }

                order.Client = client;

                await _orders.InsertOneAsync(order);

                return StatusCode(201, order);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<Order>> Update(Order order)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(x => x.Id, order.Id);

                await _orders.ReplaceOneAsync(filter, order);

                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(String id)
        {
            var orders = await _orders.Find(x => x.Id == id).FirstOrDefaultAsync();

            orders.Client = await _client.Find(x=>x.Id == orders.ClientId).FirstOrDefaultAsync();

            return orders is not null ? Ok(orders) : BadRequest();
        }

        [HttpDelete]

        public async Task<ActionResult<Order>> Delete(String id)
        {

            try
            {
                var filter = Builders<Order>.Filter.Eq(x => x.Id, id);

                await _orders.DeleteOneAsync(filter);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);

                throw;
            }
            
        }
    }
}
