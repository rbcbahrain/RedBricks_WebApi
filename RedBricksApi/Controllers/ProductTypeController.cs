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

            if (productType == null)
            {
                return BadRequest();
            }
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest("Invalid product type data.");

                string imagePath = string.Empty;

                if (productType.Image != null && productType.Image.Length > 0)
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures/Type");

                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    var fileName = Guid.NewGuid() + Path.GetExtension(productType.Image.FileName);
                    var filePath = Path.Combine(uploadDir, fileName);

                    // THIS is correct: dto.Image.CopyToAsync(stream)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productType.Image.CopyToAsync(stream);
                    }

                    imagePath = $"/Picutures/Type/{fileName}";
                }
                productType.FileName = imagePath;
                await productTypeService.AddProductType(productType);
                // Success response ALWAYS JSON
                return Ok(new
                {
                    message = "Product type created successfully"

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
            if (productType == null)
            {
                return BadRequest();
            }
            try
            {
                await productTypeService.UpdateProductType(productType);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productTypeService.DeleteProductType(id);
            return NoContent();

        }

    }
}
