using Communication.Entity.Models.Company;
using Communication.Entity.Models.User.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.Services.UserServices
{
    public interface IUserService
    {
        Task<AppUser?> getUserByToken(ClaimsPrincipal user);
        ICollection<Group> getUserGroups(AppUser user);
        Task<AppUser?> getUserById(string id);
    }
}
