using cement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cement.Controllers
{
    [ApiController]
    [Route("userController")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }
        public IActionResult Index()
        {
            return Ok();
        }

        public async Task<IActionResult> GetAction()
        {
            var Users = await _userService.CreateUsersAsync(10);
            return Ok(Users);
        }
    }
}
