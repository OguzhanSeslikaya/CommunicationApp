using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.File
{
    public class BaseFile : BaseEntity
    {
        public string fileName { get; set; }
        public string path { get; set; }
        public string storage { get; set; }
    }
}
