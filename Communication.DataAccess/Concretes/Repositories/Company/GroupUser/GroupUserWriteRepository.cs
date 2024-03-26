using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.GroupUser
{
    public class GroupUserWriteRepository : WriteRepository<Entity.Models.Company.GroupUser>, IGroupUserWriteRepository
    {
        public GroupUserWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
