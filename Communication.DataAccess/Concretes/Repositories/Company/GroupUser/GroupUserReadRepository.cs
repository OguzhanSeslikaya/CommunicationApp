using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.DataAccess.Abstractions.Repositories.Company;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.GroupUser
{
    public class GroupUserReadRepository : ReadRepository<Entity.Models.Company.GroupUser>, IGroupUserReadRepository
    {
        public GroupUserReadRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
