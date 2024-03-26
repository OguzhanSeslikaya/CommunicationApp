using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.Messages
{
    public class MessageDTO
    {
        public string sendedDate { get; set; }
        public bool didISend { get; set; }
        public string message { get; set; }
    }
}
