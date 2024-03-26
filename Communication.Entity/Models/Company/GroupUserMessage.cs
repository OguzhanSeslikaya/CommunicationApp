using Communication.Entity.Models.User.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.Company
{
    public class GroupUserMessage : BaseEntity
    {
        public Group group { get; set; }
        public string groupId { get; set; }
        public string content { get; set; }
        public AppUser sender { get; set; }
        public string senderId { get; set; }
        public AppUser receiver { get; set; }
        public string receiverId { get; set; }
    }
}
