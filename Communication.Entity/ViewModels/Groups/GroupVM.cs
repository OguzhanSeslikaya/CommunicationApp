using Communication.Entity.DTO.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.ViewModels.Groups
{
    public class GroupVM
    {
        public ICollection<GroupDTO> groups { get; set; } = new HashSet<GroupDTO>();
        public ICollection<RequestToJoinDTO> requestsToJoin { get; set; } = new List<RequestToJoinDTO>();
    }
}
