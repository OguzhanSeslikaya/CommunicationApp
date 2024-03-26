using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.ViewModels.Groups
{
    public class CallsVM
    {
        public string callFileId { get; set; }
        public string callerName { get; set; }
        public string calledName { get; set; }
        public DateTime date { get; set; }

    }
}
