using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cocktail.WebApi.Models
{
    public class UserAPI
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int UserType { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
