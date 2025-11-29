using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;

namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController(IProductTypeService productTypeService) : ControllerBase
    {



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductType productType)
        {
            await productTypeService.AddProductType(productType);
            return CreatedAtAction(nameof(GetById), new { id = productType.TypeId }, productType);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            return Ok(await productTypeService.GetProductTypes());

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductType>> GetById(int id)
        {

            return Ok(await productTypeService.GetProductTypeById(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductType productType)
        {
            await productTypeService.UpdateProductType(productType);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productTypeService.DeleteProductType(id);
            return NoContent();

        }

    }
}
