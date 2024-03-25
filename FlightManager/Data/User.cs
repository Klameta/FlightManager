using Microsoft.AspNetCore.Identity;
using System;

namespace Data
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
    }
}
