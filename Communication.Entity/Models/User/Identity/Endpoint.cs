using Communication.Entity.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.User.Identity
{
    public class Endpoint : BaseEntity
    {
        public string permission { get; set; }
    }
}
