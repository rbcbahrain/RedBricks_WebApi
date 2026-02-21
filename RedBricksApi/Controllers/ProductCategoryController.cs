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


                bool result = await productCategoryService.CheckCategoryExistAsync(productCategory.Id,productCategory.Name);

                if (result == true)
                {
                    Console.WriteLine(productCategory.Name + "Already Exist");
                    return Conflict(new { message = "Name already exists" });

                }
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

                    imagePath = $"/Pictures/Category/{fileName}";
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
            try
            {
                return Ok(await productCategoryService.GetProductCategories());
            }
            catch (Exception)
            {

                throw;
            }
           

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductCategory>> GetById(int id)
        {

            return Ok(await productCategoryService.GetProductCategoryById(id));
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductCategory productCategory)
         {
            if (productCategory == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest("Invalid product category data.");

            try
            {

                bool result = await productCategoryService.CheckCategoryExistAsync(id, productCategory.Name);

                if (result == true)
                {
                    Console.WriteLine(productCategory.Name+ "Already Exist");
                    return Conflict(new { message = "Name already exists" });
                   
                }

                productCategory.Id = id;

                string imagePath = string.Empty;
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Category");

                // Ensure the folder exists
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                if (productCategory.Image != null && productCategory.Image.Length > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(productCategory.FileName))
                    {
                        var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productCategory.FileName.TrimStart('/').Replace("/", "\\"));
                        if (System.IO.File.Exists(oldFilePath))
                            System.IO.File.Delete(oldFilePath);
                    }

                    // Ensure extension exists
                    var extension = Path.GetExtension(productCategory.Image.FileName);
                    if (string.IsNullOrEmpty(extension))
                        extension = ".png";

                    string[] fileRef = productCategory.FileName.Split('/');
                    var fileName = fileRef[3]; // Guid.NewGuid() + extension;
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productCategory.Image.CopyToAsync(stream);
                    }

                    // Correct URL path
                    imagePath = $"/Pictures/Category/{fileName}";
                    productCategory.FileName = imagePath;
                }

               

                await productCategoryService.UpdateProductCategory(productCategory);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception to debug
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
                string fileName= strrefval[1].Trim();

                if (fileName != "")
                {
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Category");

                    var oldFilePath = Path.Combine(uploadDir, fileName);
                    System.IO.File.Delete(oldFilePath);
                }

                await productCategoryService.DeleteProductCategory(id);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
        //[HttpGet("CheckCategory")]
        //public async Task<bool> CheckCategoryExits(string categoryName)
        //{
        //    try
        //    {
        //        bool result = await productCategoryService.CheckCategoryExistAsync(categoryName);
        //        return result;  
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
