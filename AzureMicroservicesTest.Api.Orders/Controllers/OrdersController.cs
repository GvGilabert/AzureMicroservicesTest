using AzureMicroservicesTest.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureMicroservicesTest.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider _ordersProvider)
        {
            ordersProvider = _ordersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await ordersProvider.GetOrdersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var result = await ordersProvider.GetOrderAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Order);
            }
            return NotFound();
        }
    }
}
