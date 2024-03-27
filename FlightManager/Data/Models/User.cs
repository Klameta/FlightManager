﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Data.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
    }
}
