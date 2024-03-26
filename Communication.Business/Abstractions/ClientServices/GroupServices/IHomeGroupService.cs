using Communication.Entity.ViewModels.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.Services.GroupServices
{
    public interface IHomeGroupService
    {
        Task<MyGroupVM?> createGroupAsync(string groupName,ClaimsPrincipal user);
        Task<ICollection<MyGroupVM>> getUserGroupsAsync(ClaimsPrincipal user);
        Task<bool> leaveGroupAsync(MyGroupVM myGroupVM,ClaimsPrincipal user);
        Task<GroupVM> groupsAndUserRequests(ClaimsPrincipal user);
        Task<bool> RequestToJoinGroup(ClaimsPrincipal user,string groupId);
        Task<bool> removeRequest(ClaimsPrincipal user, string requestId);
    }
}
