using Communication.Entity.ViewModels.Groups;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.ClientServices.GroupServices
{
    public interface IGroupSettingsService
    {
        Task<ICollection<RequestsToJoinGroupVM>> getRequestsToJoinAsync(string groupId);
        Task<bool> declineRequestToJoinAsync(string groupId);
        Task<bool> acceptRequestToJoinAsync(string groupId);
        Task<ICollection<MemberSettingsVM>> getMembers(string groupId);
        Task<bool> kickMember(string userId, string groupId);
        Task checkVoicesAsync();
        Task<string> getVoiceFilePathById(string id);
        Task<ICollection<CallsVM>> getCalls(string groupId);
        
    }
}
