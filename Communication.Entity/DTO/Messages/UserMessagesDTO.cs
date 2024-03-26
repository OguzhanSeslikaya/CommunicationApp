using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.Messages
{
    public class UserMessagesDTO
    {
        public string targetUserName { get; set; }
        public string targetUserId { get; set; }
        public ICollection<MessageDTO> messages { get; set; }
    }
}
