using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;
using RedBricksApi.Repository.Services;

namespace RedBricksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("getuserlist")]
        public async Task<IEnumerable<User>> GetUserListAsync()
        {
            try
            {
                return await userService.GetUserListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getuserById")]
        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                return await userService.GetUserByIdAsync(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            //await userService.AddNewUserAsync(user);
            //return CreatedAtAction(nameof(GetUserByIdAsync), new { id = user.UserId }, user);
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid user data." });
                }

                // Add user
                await userService.AddNewUserAsync(user);

                // Success response ALWAYS JSON
                return Ok(new
                {
                    message = "User created successfully"

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
        //[HttpPost("adduser")]
        //public async Task<IActionResult> AddUserAsync(User user)
        //{

        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var response = await userService.AddNewUserAsync(user);
        //        return Ok(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        [HttpPut("updateuser")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid user data." });
                }

                // Add user
                await userService.UpdateUserAsync(user);

                // Success response ALWAYS JSON
                return Ok(new
                {
                    message = "User updated successfully"

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

        //[HttpPut("updateuser")]
        //public async Task<IActionResult> UpdateUserAsync(User user)
        //{

        //    if (user == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var response = await userService.UpdateUserAsync(user);
        //        return Ok(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        [HttpDelete("deleteuser")]
        public async Task<ActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                await userService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("checkuserexist")]
        public async Task<User> CheckUserExistAsync(string username, string userpassword)
        {
            try
            {

                var user = await userService.CheckUserExistAsync(username, userpassword);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
