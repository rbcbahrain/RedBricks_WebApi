using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;


namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController( ICartService cartService) : ControllerBase
    {



        [HttpPost("AddCart")]
        public async Task<IActionResult> Create([FromBody] CartItems cartItems)
        {
           
            int cartId = await cartService.AddCart(cartItems.UserId);
            if (cartId != 0)
            {
                cartItems.CartId = cartId;

                await cartService.AddCartItems(cartItems);
                return Ok(new
                {
                    message = "Cart added successfully"

                });
            }
            return Ok(new
            {
                message = "Cart added failed"

            });

        }

        [HttpGet("GetCartlist")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return Ok(await cartService.GetCarts());

        }
        [HttpGet("GetCartById/{id:int}")]
        //[HttpGet]
        public async Task<ActionResult<Cart>> GetById(int id)
        {

            return Ok(await cartService.GetCartById(id));
        }

        [HttpPut("updateCart")]
        public async Task<IActionResult> Update([FromBody] Cart cart)
        {
            await cartService.UpdateCart(cart);
            return NoContent();

        }
        [HttpDelete("DeleteCart/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await cartService.DeleteCartById(id);
            return NoContent();

        }

        [HttpPost("AddCartItems")]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItems cartitems)
        {
            await cartService.AddCartItems(cartitems);
            return CreatedAtAction(nameof(GetById), new { id = cartitems.Id }, cartitems);
        }

        [HttpGet("GetCartItemslist")]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetCartItems(int userId)
        {
            return Ok(await cartService.GetCartItems(userId));

        }
        [HttpGet("GetCartItemsById/{id:int}")]
        
        public async Task<ActionResult<CartItems>> GetCartItemsById(int id)
        {

            return Ok(await cartService.GetCartItemsById(id));
        }

        [HttpPut("updateCartItem")]
        public async Task<IActionResult> Update([FromBody] CartItems cartitems)
        {
            await cartService.UpdateCartItems(cartitems);
            return NoContent();

        }
        [HttpDelete("DeleteCartItem/{id:int}")]
        //[HttpDelete]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            await cartService.DeleteCartItems(id);
            return NoContent();

        }


    }
}
