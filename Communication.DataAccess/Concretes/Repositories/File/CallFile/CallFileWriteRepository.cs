using Communication.DataAccess.Abstractions.Repositories.File.CallFile;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.File.CallFile
{
    public class CallFileWriteRepository : WriteRepository<Entity.Models.File.LocalStorage.CallFile>, ICallFileWriteRepository
    {
        public CallFileWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
