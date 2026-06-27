using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace cement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
