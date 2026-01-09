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
            try
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
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("GetCartlist")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            try
            {
                return Ok(await cartService.GetCarts());
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        [HttpGet("GetCartById/{id:int}")]
        public async Task<ActionResult<Cart>> GetCartById(int id)
        {
            try
            {
                return Ok(await cartService.GetCartById(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("updateCart")]
        public async Task<IActionResult> Update([FromBody] Cart cart)
        {
            try
            {
                await cartService.UpdateCart(cart);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("DeleteCart/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await cartService.DeleteCartById(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("AddCartItems")]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItems cartitems)
        {
            try
            {
                await cartService.AddCartItems(cartitems);
                return CreatedAtAction(nameof(GetCartItemsById), new { id = cartitems.CartId }, cartitems);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpGet("GetCartItemslist")]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetCartItems(int userId)
        {
            try
            {
                return Ok(await cartService.GetCartItems(userId));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetCartItemsById/{id:int}")]
        
        public async Task<ActionResult<CartItems>> GetCartItemsById(int id)
        {
            try
            {
                return Ok(await cartService.GetCartItemsById(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("updateCartItem")]
        public async Task<IActionResult> Update([FromBody] CartItems cartitems)
        {
            try
            {
                await cartService.UpdateCartItems(cartitems);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("DeleteCartItem/{id:int}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            try
            {
                await cartService.DeleteCartItems(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
