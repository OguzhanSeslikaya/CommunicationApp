using Communication.Entity.Models.User.UserPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess.Abstractions.Repositories.User.UserPosts
{
    public interface IUserPostReadRepository : IReadRepository<UserPost>
    {
    }
}
