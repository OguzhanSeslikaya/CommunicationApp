using Communication.DataAccess.Abstractions.Repositories.Company.GroupUserMessage;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.GroupUserMessage
{
    public class GroupUserMessageWriteRepository : WriteRepository<Entity.Models.Company.GroupUserMessage>, IGroupUserMessageWriteRepository
    {
        public GroupUserMessageWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
