using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;


namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {

        [HttpPost("CheckOut")]
        public async Task<IActionResult> Create([FromBody] int userId)
        {
            try
            {
                await orderService.AddOrders(userId);
                return Ok(new
                {
                    message = "Order checkout successfully"
                });

            }
            catch (Exception)
            {
                return Ok(new
                {
                    message = "Order Checkout failed"

                });
               // throw;
            }
           
        }
        [HttpGet("GetOrderlist")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders(int userId)
        {
            try
            {
                return Ok(await orderService.GetOrderList(userId));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetOrder{OrderId:int}")]
        public async Task<ActionResult<Orders>> GetOrderById(int orderId)
        {
            try
            {
                return Ok(await orderService.GetOrdersById(orderId));
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpPut("updateOrder")]
        public async Task<IActionResult> Update([FromBody] Orders orders)
        {
            try
            {
                await orderService.UpdateOrders(orders);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("DeleteOrder{id}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            try
            {
                await orderService.DeleteOrdersById(orderId);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
        
        
        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> Create([FromBody] OrderItems orderitem)
        {
            try
            {
                await orderService.AddOrderItems(orderitem);
                return CreatedAtAction(nameof(GetOrderItemById), new { id = orderitem.OrderItemId }, orderitem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetOrderItemlist")]
        public async Task<ActionResult<IEnumerable<OrderItems>>> GetOrderItmesList(int orderId)
        {
            try
            {
                return Ok(await orderService.GetOrderItemsList(orderId));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetOrderItem{id:int}")]
        public async Task<ActionResult<OrderItems>> GetOrderItemById(int orderItemId)
        {
            try
            {
                return Ok(await orderService.GetOrderItemsById(orderItemId));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("updateOrderItem")]
        public async Task<IActionResult> Update([FromBody] OrderItems orderitem)
        {
            try
            {
                await orderService.UpdateOrderItems(orderitem);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("DeleteOrderItem{id}")]
        public async Task<IActionResult> DeleteOrderItem(int orderItemId)
        {
            try
            {
                await orderService.DeleteOrderItem(orderItemId);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
            

        }


    }
}
