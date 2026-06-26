using cement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace cement.Interfaces
{
    internal interface IUserService
    {
        Task<ServiceResponse<string>> GetUsernameAsync(int userId);
        Task<ServiceResponse<List<User>>> CreateUsersAsync(int amount);
        Task<ServiceResponse<User>> GetUserByNameAsync(string name, List<User> users);
    }
}
