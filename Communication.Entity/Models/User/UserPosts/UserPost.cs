using Communication.Entity.Models.Company;
using Communication.Entity.Models.File.LocalStorage;
using Communication.Entity.Models.User.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.User.UserPosts
{
    public class UserPost : BaseEntity
    {
        public string content { get; set; }
        public AppUser user { get; set; }
        public string userId { get; set; }
        public Group group { get; set; }
        public string groupId { get; set; }
        public PostFile? postFile { get; set; }
        public string? postFileId { get; set; }
    }
}
