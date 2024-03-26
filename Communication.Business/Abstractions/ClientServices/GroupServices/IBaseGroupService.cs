using Communication.Entity.Models.Company;
using Communication.Entity.Models.User.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.ClientServices.GroupServices
{
    public interface IBaseGroupService
    {
        Task<Group?> getGroupById(string id);
        Task<Group?> getGroupByTokenWithId(ClaimsPrincipal user,string id);
        Task<Group?> getGroupByTokenWithId(AppUser user, string id);
    }
}
