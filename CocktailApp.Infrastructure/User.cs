using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailApp.Infrastructure
{
    public partial class User
    {
        [Key]
        public int PkId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserTypeEnum UserType { get; set; }
    }
}
