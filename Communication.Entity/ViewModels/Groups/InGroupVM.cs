using Communication.Entity.DTO.Groups;
using Communication.Entity.DTO.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.ViewModels.Groups
{
    public class InGroupVM
    {
        public ICollection<GroupUserDTO> groupUsers {  get; set; }
        public ICollection<PostDTO>? postUsers { get; set; }
        public string userId { get; set; }
        public bool isAuthority { get; set; }
    }
}
