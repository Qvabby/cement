using cement.Models;

namespace cement.Models.DTOs
{
    public class UserDTO : User
    {
        public required string NameDTO { get; set; }
        public required string UserNameDTO { get; set; }
        public required DateTime CreatedAtDTO { get; set; }

    }
}
