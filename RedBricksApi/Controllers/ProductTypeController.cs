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



        [HttpPost("AddType")]
        public async Task<IActionResult> Create([FromForm] ProductType productType)
        {

            if (productType == null)
            {
                return BadRequest();
            }
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest("Invalid product type data.");

                bool result = await productTypeService.CheckProductTypeExistAsync(productType.TypeId, productType.Name);
                if (result == true)
                {
                    Console.WriteLine(productType.Name + "Already Exist");
                    return Conflict(new { message = "Name already exists" });
                }

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

                    imagePath = $"/Pictures/Type/{fileName}";
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

        [HttpGet("GetTypeList")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
        {
            try
            {
                return Ok(await productTypeService.GetProductTypes());
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductType>> GetById(int id)
        {
            try
            {
                return Ok(await productTypeService.GetProductTypeById(id));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpPut("UpdateType/{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] ProductType productType)
        {
            if (productType == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalide product type data");
            }

            try
            {
                bool result = await productTypeService.CheckProductTypeExistAsync(id, productType.Name);
                if (result == true)
                {
                    Console.WriteLine(productType.Name + "Already Exist");
                    return Conflict(new { message = "Name already exists" });
                }

                productType.TypeId = id;

                string imagePath = string.Empty;
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Type");

                // Ensure the folder exists
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                if (productType.Image != null && productType.Image.Length > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(productType.FileName))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productType.FileName.TrimStart('/').Replace("/", "\\"));
                        if (System.IO.File.Exists(oldFilePath))
                            System.IO.File.Delete(oldFilePath);
                    }

                    // Ensure extension exists
                    var extension = Path.GetExtension(productType.Image.FileName);
                    if (string.IsNullOrEmpty(extension))
                        extension = ".png";

                    string[] fileRef = productType.FileName.Split('/');
                    var fileName = fileRef[3]; // Guid.NewGuid() + extension;
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productType.Image.CopyToAsync(stream);
                    }

                    // Correct URL path
                    imagePath = $"/Pictures/Category/{fileName}";
                    productType.FileName = imagePath;
                }
                await productTypeService.UpdateProductType(productType);
                return NoContent();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("Delete/{strRef}")]
        public async Task<IActionResult> Delete(string strRef)
        {
            try
            {
                string[] strrefval = strRef.Split(',');
                int id = Convert.ToInt32(strrefval[0].Trim());
                string fileName = strrefval[1].Trim();

                if (fileName != "")
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Type");

                    var oldFilePath = Path.Combine(uploadDir, fileName);
                    System.IO.File.Delete(oldFilePath);
                } 

                await productTypeService.DeleteProductType(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
        //[HttpGet("CheckProductType")]
        //public async Task<bool> CheckProductTypeExits(string productTypeName)
        //{
        //    try
        //    {
        //        bool result = await productTypeService.CheckProductTypeExistAsync(productTypeName);
        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


    }
}
