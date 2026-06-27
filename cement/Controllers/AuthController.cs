using cement.Interfaces;
using cement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace cement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
           
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
            
            
        }

    }
}
