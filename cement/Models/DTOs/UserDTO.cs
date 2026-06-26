using cement.Models;

namespace cement.Models.DTOs
{
    public class UserDTO : User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
