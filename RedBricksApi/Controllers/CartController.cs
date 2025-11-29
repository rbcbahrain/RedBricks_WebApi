using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;


namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {

        [HttpPost("AddCart")]
        public async Task<IActionResult> Create([FromBody] Cart cart)
        {
            await cartService.AddCart(cart);
            return CreatedAtAction(nameof(GetById), new { id = cart.Id }, cart);
        }

        [HttpGet("GetCartlist")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return Ok(await cartService.GetCarts());

        }
        [HttpGet("{id:int}")]
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await cartService.DeleteCartById(id);
            return NoContent();

        }



    }
}
