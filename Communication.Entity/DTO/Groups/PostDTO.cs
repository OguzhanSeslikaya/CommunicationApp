using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.DTO.Groups
{
    public class PostDTO
    {
        public string id { get; set; }
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string createdDate { get; set; }
        public GroupUserDTO user { get; set; }
        public string content { get; set; }
        
    }
}
