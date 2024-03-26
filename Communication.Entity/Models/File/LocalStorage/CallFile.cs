using Communication.Entity.Models.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.File.LocalStorage
{
    public class CallFile : BaseFile
    {
        public Call call { get; set; }
        public string callId { get; set; }
        public bool isMixed { get; set; }
    }
}