using cement.Models.DTOs;

namespace cement.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> LoginAsync(LoginDTO request);
    }
}
