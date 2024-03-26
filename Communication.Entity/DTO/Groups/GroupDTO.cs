using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.Groups
{
    public class GroupDTO
    {
        public string id { get; set; }
        public string groupName { get; set; }
        public int numberOfMembers { get; set; }
    }
}
