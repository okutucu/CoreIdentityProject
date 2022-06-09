using System;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentityProject.Models
{
    public class AppUser : IdentityUser
    {

        public string City { get; set; }
        public string Picture { get; set; }
        public DateTime? BirthDay { get; set; }
        public byte Gender { get; set; }
    }
}
