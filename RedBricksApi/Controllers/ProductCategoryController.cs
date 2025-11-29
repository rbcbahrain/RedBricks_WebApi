using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;

namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController(IProductCategoryService productCategoryService) : ControllerBase
    {



        [HttpPost("AddCategory")]
        public async Task<IActionResult> Create([FromBody] ProductCategory productCategory)
        {
            await productCategoryService.AddProductCategory(productCategory);
            return CreatedAtAction(nameof(GetById), new { id = productCategory.Id }, productCategory);
        }

        [HttpGet("GetCategorylist")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategory()
        {
            return Ok(await productCategoryService.GetProductCategories());

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductCategory>> GetById(int id)
        {

            return Ok(await productCategoryService.GetProductCategoryById(id));
        }

        [HttpPut ("UpdateCategory")]
        public async Task<IActionResult> Update([FromBody] ProductCategory productCategory)
        {
            await productCategoryService.UpdateProductCategory(productCategory);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productCategoryService.DeleteProductCategory(id);
            return NoContent();

        }

    }
}
