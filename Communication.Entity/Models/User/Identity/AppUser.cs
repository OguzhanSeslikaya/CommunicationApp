using Communication.Entity.Models.Company;
using Communication.Entity.Models.User.UserPosts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.User.Identity
{
    public class AppUser : IdentityUser
    {
        public bool isAdmin { get; set; } = false;
        public ICollection<UserPost> posts { get; set; }
    }
}
