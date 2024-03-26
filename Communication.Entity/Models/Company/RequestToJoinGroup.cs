using Communication.Entity.Enums.Group;
using Communication.Entity.Models.User.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.Company
{
    public class RequestToJoinGroup : BaseEntity
    {
        public AppUser user { get; set; }
        public string userId { get; set; }
        public Group group { get; set; }
        public string groupId { get; set; }
        public RequestToJoin state { get; set; }
    }
}
