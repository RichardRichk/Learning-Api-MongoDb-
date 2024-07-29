using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
    }
}
