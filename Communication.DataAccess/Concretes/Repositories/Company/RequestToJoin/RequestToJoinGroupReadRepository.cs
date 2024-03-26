using Communication.DataAccess.Abstractions.Repositories.Company.RequestToJoin;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.RequestToJoin
{
    public class RequestToJoinGroupReadRepository : ReadRepository<Entity.Models.Company.RequestToJoinGroup>, IRequestToJoinGroupReadRepository
    {
        public RequestToJoinGroupReadRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
