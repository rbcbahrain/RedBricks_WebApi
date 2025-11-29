using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;


namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController(ICartItemService cartItemService) : ControllerBase
    {

        [HttpPost("AddCartItems")]
        public async Task<IActionResult> Create([FromBody] CartItems cartitems)
        {
            await cartItemService.AddCartItems(cartitems);
            return CreatedAtAction(nameof(GetById), new { id = cartitems.Id }, cartitems);
        }

        [HttpGet("GetCartItemslist")]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetCartItems()
        {
            return Ok(await cartItemService.GetCartItems());

        }
        [HttpGet("{id:int}")]
        //[HttpGet]
        public async Task<ActionResult<CartItems>> GetById(int id)
        {

            return Ok(await cartItemService.GetCartItemsById(id));
        }

        [HttpPut("updateCartItem")]
        public async Task<IActionResult> Update([FromBody] CartItems cartitems)
        {
            await cartItemService.UpdateCartItems(cartitems);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await cartItemService.DeleteCartItems(id);
            return NoContent();

        }



    }
}
