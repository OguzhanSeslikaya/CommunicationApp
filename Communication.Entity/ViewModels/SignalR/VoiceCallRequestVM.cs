using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.ViewModels.SignalR
{
    public class VoiceCallRequestVM
    {
        public string senderId { get; set; }
        public string senderName { get; set; }
        public string groupId { get; set; }
        public string groupName { get; set; }
        public string receiverId { get; set; }
    }
}
