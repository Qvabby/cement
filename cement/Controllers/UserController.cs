using cement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAction()
        {
            var Users = await _userService.CreateUsersAsync(10);
            return View(Users);
        }
    }
}
