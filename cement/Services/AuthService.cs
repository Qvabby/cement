using cement.Data;
using cement.Interfaces;
using cement.Models;
using cement.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cement.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginDTO request)
        {
            try
            {
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.UserName == request.Username);

                if (user == null)
                    return new ServiceResponse<string>
                    { Success = false, Description = "User Not Found."};

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                    return new ServiceResponse<string> { Success = false, Description = "Wrong password" };

                var token = GenerateToken(user);
                return new ServiceResponse<string>
                {
                    Data = token,
                    Success = true,
                    Description = "Login Successful."
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Description = $"An error occurred during login: {e.Message}"
                };
            }
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
