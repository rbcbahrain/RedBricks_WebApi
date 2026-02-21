using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;

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
                bool result = await productService.CheckProductExistAsync(product.ProductId, product.Name);

                if (result == true)
                {
                    Console.WriteLine(product.Name + "already Exist");
                    return Conflict(new { message = "Name already exists" });

                }

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
        [HttpPut("updateproduct/{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest("Invalid product data.");

                try
                {

                    bool result = await productService.CheckProductExistAsync(id, product.Name);

                    if (result == true)
                    {
                        Console.WriteLine(product.Name + "already Exist");
                        return Conflict(new { message = "Name already exists" });

                    }

                    product.ProductId = id;

                    string imagePath = string.Empty;
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Product");

                    // Ensure the folder exists
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    if (product.Image != null && product.Image.Length > 0)
                    {
                        // Delete old image if exists
                        if (!string.IsNullOrEmpty(product.FileName))
                        {
                            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.FileName.TrimStart('/').Replace("/", "\\"));
                            if (System.IO.File.Exists(oldFilePath))
                                System.IO.File.Delete(oldFilePath);
                        }

                        // Ensure extension exists
                        var extension = Path.GetExtension(product.Image.FileName);
                        if (string.IsNullOrEmpty(extension))
                            extension = ".png";

                        string[] fileRef = product.FileName.Split('/');
                        var fileName = fileRef[3]; // Guid.NewGuid() + extension;
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.Image.CopyToAsync(stream);
                        }

                        // Correct URL path
                        imagePath = $"/Pictures/Category/{fileName}";
                        product.FileName = imagePath;
                    }

                    await productService.UpdateProductAsync(product);
                    return NoContent();        
                }
                catch (Exception)
                {

                    throw;
                }
        }
        [HttpDelete("Deleteproduct/{strRef}")]
        public async Task<IActionResult> DeleteProductAsync(string strRef)
        {
            try
            {
                string[] strrefval = strRef.Split(',');
                int id = Convert.ToInt32(strrefval[0].Trim());
                string fileName = strrefval[1].Trim();

                if (fileName != "")
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Product");

                    var oldFilePath = Path.Combine(uploadDir, fileName);
                    System.IO.File.Delete(oldFilePath);
                }

                await productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpGet("CheckProduct")]
        //public async Task<bool> CheckProductExits(string productName)
        //{
        //    try
        //    {
        //        bool result = await productService.CheckProductExistAsync(productName);
        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


    }
}
