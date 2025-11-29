using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;


namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        [HttpPost("AddOrder")]
        public async Task<IActionResult> Create([FromBody] Orders orders)
        {
            await orderService.AddOrders(orders);
            return CreatedAtAction(nameof(GetById), new { id = orders.Id }, orders);
        }

        [HttpGet("GetOrderlist")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            return Ok(await orderService.GetOrderList());

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Orders>> GetById(int id)
        {

            return Ok(await orderService.GetOrdersById(id));
        }

        [HttpPut("updateOrder")]
        public async Task<IActionResult> Update([FromBody] Orders orders)
        {
            await orderService.UpdateOrders(orders);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderService.DeleteOrdersById(id);
            return NoContent();

        }



    }
}
