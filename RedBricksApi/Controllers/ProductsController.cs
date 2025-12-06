using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController (IProductService productService) : ControllerBase
    {
       
        [HttpGet("getproductList")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductListasync()
        {
            try
            {
                return Ok(await productService.GetProductListAsync());

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getproductbyid")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int Id)
        {
            try
            {
               return Ok(await productService.GetProductByIdAsync(Id));
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest("Invalid product data.");

                string imagePath=string.Empty;

                if (product.Image != null && product.Image.Length > 0)
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures/Product");
                   
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    var fileName = Guid.NewGuid() + Path.GetExtension(product.Image.FileName);
                    var filePath = Path.Combine(uploadDir, fileName);

                    // THIS is correct: dto.Image.CopyToAsync(stream)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.Image.CopyToAsync(stream);
                    }

                    imagePath = $"/Picutures/Product/{fileName}";
                }
                product.FileName = imagePath;
                await productService.AddProductAsync(product);
                // Success response ALWAYS JSON
                return Ok(new
                {
                    message = "Service created successfully"

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
        [HttpPut("updateproduct")]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            try
            {
                await productService.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("deleteproduct")]
        public async Task<IActionResult> DeleteProductAsync(int Id)
        {
            try
            {
                await productService.DeleteProductAsync(Id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
