using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.SignalR
{
    public class SignalRVoiceCallReceiveDTO
    {
        public string senderId { get; set; }
        public string receiverId { get; set; }
        public string callId { get; set; }
    }
}
