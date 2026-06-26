using cement.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> GetFakeUsers()
        {
            var Users = await _userService.CreateUsersAsync(10);
            return Ok(Users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] cement.Models.User user)
        {
            var response = await _userService.AddUserAsync(user);
            if (!response.Success)
            {
                return BadRequest(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet]
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
