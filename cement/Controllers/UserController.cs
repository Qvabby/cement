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
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _userService.CreateUsersAsync(10);
            return Ok(Users);
        }
    }
}
