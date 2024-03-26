using Communication.DataAccess.Abstractions.Repositories.Company.Call;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.Company.Call
{
    public class CallReadRepository : ReadRepository<Entity.Models.Company.Call>, ICallReadRepository
    {
        public CallReadRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
