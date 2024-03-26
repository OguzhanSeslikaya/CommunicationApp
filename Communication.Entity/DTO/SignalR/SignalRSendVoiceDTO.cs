using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.SignalR
{
    public class SignalRSendVoiceDTO
    {
        public string voice { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
    }
}
