using Communication.DataAccess.Abstractions.Repositories.Company.GroupUserMessage;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.GroupUserMessage
{
    public class GroupUserMessageReadRepository : ReadRepository<Entity.Models.Company.GroupUserMessage>, IGroupUserMessageReadRepository
    {
        public GroupUserMessageReadRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
