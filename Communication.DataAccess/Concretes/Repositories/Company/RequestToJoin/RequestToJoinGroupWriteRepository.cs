using Communication.DataAccess.Abstractions.Repositories.Company.RequestToJoin;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.RequestToJoin
{
    public class RequestToJoinGroupWriteRepository : WriteRepository<Entity.Models.Company.RequestToJoinGroup>, IRequestToJoinGroupWriteRepository
    {
        public RequestToJoinGroupWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
