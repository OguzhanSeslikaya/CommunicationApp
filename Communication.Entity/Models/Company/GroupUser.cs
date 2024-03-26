using Communication.Entity.Models.User.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.Company
{
    public class GroupUser : BaseEntity
    {
        public Group group { get; set; }
        public string groupId { get; set; }
        public AppUser user { get; set; }
        public string userId { get; set; }
    }
}
