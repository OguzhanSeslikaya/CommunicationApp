using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.ViewModels.Groups
{
    public class CreatePostVM
    {
        public string groupId { get; set; }
        public string content { get; set; }
        public IFormFile file { get; set; }

    }
}
