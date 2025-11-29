using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;


namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController(IOrderItemService orderItemService) : ControllerBase
    {

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> Create([FromBody] OrderItems orderitem)
        {
            await orderItemService.AddOrderItems(orderitem);
            return CreatedAtAction(nameof(GetById), new { id = orderitem.Id }, orderitem);
        }

        [HttpGet("GetOrderItemlist")]
        public async Task<ActionResult<IEnumerable<OrderItems>>> GetOrders()
        {
            return Ok(await orderItemService.GetOrderItemsList());

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItems>> GetById(int id)
        {

            return Ok(await orderItemService.GetOrderItemsById(id));
        }

        [HttpPut("updateOrderItem")]
        public async Task<IActionResult> Update([FromBody] OrderItems orderitem)
        {
            await orderItemService.UpdateOrderItems(orderitem);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderItemService.GetOrderItemsById(id);
            return NoContent();

        }



    }
}
