using Communication.DataAccess.Abstractions.Repositories.User.UserPosts;
using Communication.DataAccess.Contexts;
using Communication.Entity.Models.User.UserPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Concretes.Repositories.User.UserPosts
{
    public class UserPostWriteRepository : WriteRepository<UserPost>, IUserPostWriteRepository
    {
        public UserPostWriteRepository(CommunicationAppDBContext context) : base(context)
        {
        }
    }
}
