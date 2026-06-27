using cement.Interfaces;
using cement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet("getFakeUsers")]
        public async Task<IActionResult> GetFakeUsers()
        {
            var Users = await _userService.CreateUsersAsync(10);
            return Ok(Users);
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var response = await _userService.AddUserAsync(user);
            if (!response.Success)
            {
                return BadRequest(response.Description);
            }   
            return Ok(response.Data);
        }

        [Authorize]
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsersAsync();
            if (!response.Success)
            {
                return BadRequest(response.Description);
            }
            return Ok(response.Data);
        }
    }
}
