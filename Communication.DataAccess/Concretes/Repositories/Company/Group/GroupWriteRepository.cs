using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.Group
{
    public class GroupWriteRepository : WriteRepository<Entity.Models.Company.Group>, IGroupWriteRepository
    {
        public GroupWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
