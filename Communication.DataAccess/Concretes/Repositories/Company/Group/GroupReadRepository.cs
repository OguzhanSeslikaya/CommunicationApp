using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.Group
{
    public class GroupReadRepository : ReadRepository<Entity.Models.Company.Group>, IGroupReadRepository
    {
        public GroupReadRepository(CommunicationAppDBContext context) : base(context)
        {
        }

    }
}
