using Communication.Entity.Models.User.Identity;
using Communication.Entity.Models.User.UserPosts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.Company
{
    public class Group : BaseEntity
    {
        public string name { get; set; }
        public AppUser creator { get; set; }
        public string creatorId { get; set; }
    }
}
