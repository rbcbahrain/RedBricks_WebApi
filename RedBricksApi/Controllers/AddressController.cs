using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;

namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IAddressService addressService) : ControllerBase
    {
        [HttpPost("addAddress")]
        public async Task<IActionResult> Create([FromBody] Address address)
        { 
            await addressService.AddAddress(address);
            return CreatedAtAction(nameof(GetById), new { id = address.AddressId}, address);
        }

        [HttpGet("Getaddresslist/{userid}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress(int userid)
        {
            return Ok(await addressService.GetAddress(userid));

        }
        [HttpGet("{id:int}")]
        //[HttpGet]
        public async Task<ActionResult<Address>> GetById(int id)
        {

            return Ok(await addressService.GetAddressById(id));
        }

        [HttpPut("updateAddress")]
        public async Task<IActionResult> Update([FromBody] Address address)
        {
            await addressService.UpdateAddress(address);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await addressService.DeleteAddress(id);
            return NoContent();

        }

    }
}
