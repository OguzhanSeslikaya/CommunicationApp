using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity
{
    public class BaseEntity
    {
        public virtual string id { get; set; }
        public DateTime createdDate { get; set; }
    }
}
