using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.ViewModels.Groups
{
    public class MyGroupVM
    {
        public string id { get; set; }
        public string groupName { get; set; }
        public string myRole { get; set; }
        public int groupMemberCount { get; set; }
    }
}
