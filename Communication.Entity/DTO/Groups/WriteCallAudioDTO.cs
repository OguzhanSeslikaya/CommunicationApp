using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.Groups
{
    public class WriteCallAudioDTO
    {
        public string data { get; set; }
        public string groupId { get; set; }
        public string callId { get; set; }
    }
}
