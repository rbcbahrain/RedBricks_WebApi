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
        public async Task<IActionResult> Create([FromForm] ProductCategory productCategory)
        {
            
                       if (productCategory == null)
            {
                return BadRequest();
            }
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest("Invalid product data.");

                string imagePath = string.Empty;

                if (productCategory.Image != null && productCategory.Image.Length > 0)
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures/Category");

                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    var fileName = Guid.NewGuid() + Path.GetExtension(productCategory.Image.FileName);
                    var filePath = Path.Combine(uploadDir, fileName);

                    // THIS is correct: dto.Image.CopyToAsync(stream)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productCategory.Image.CopyToAsync(stream);
                    }

                    imagePath = $"/Picutures/Category/{fileName}";
                }
                productCategory.FileName = imagePath;
                await productCategoryService.AddProductCategory(productCategory);
                // Success response ALWAYS JSON
                return Ok(new
                {
                    message = "Category created successfully"

                });
            }
            catch (Exception ex)
            {
                // Error response ALWAYS JSON
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }



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
