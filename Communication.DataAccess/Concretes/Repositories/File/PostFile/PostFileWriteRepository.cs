using Communication.DataAccess.Abstractions.Repositories.File.PostFile;
using Communication.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.File.PostFile
{
    public class PostFileWriteRepository : WriteRepository<Entity.Models.File.LocalStorage.PostFile>, IPostFileWriteRepository
    {
        public PostFileWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
