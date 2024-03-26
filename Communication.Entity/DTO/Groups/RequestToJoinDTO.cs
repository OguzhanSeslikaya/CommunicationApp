using Communication.Entity.Enums.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.Groups
{
    public class RequestToJoinDTO
    {
        public string id { get; set; }
        public string groupName { get; set; }
        public string state { get; set; }
    }
}
