using Communication.Entity.Models.File.LocalStorage;
using Communication.Entity.Models.User.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Entity.Models.Company
{
    public class Call : BaseEntity
    {
        public CallFile? mainCallFile { get; set; }
        [ForeignKey("mainCallFile")]
        public string? mainCallFileId { get; set; }
        public AppUser caller { get; set; }
        public string callerId { get; set; }
        public AppUser called { get; set; }
        public string calledId { get; set; }
        public Group group { get; set; }
        public string groupId { get; set; }
        public ICollection<CallFile>? callFiles { get; set; }
    }
}
