using Communication.Entity.DTO.Groups;
using Communication.Entity.DTO.Messages;
using Communication.Entity.ViewModels.Groups;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Abstractions.Services.GroupServices
{
    public interface IGroupService
    {
        Task<ICollection<GroupUserDTO>> getAllUsers(string groupId,ClaimsPrincipal user);
        Task<bool> createPostAsync(CreatePostVM createPostVM, ClaimsPrincipal user);
        Task<ICollection<PostDTO>?> getAllUserPostsInGroup(ClaimsPrincipal user, string groupId);
        Task<string?> getGroupNameByGroupId(ClaimsPrincipal user, string groupId);
        Task<string?> getPostFileNameByPostFileId(string fileId);
        Task<UserMessagesDTO> getUserMessagesAsync(ClaimsPrincipal user, string groupId,string targetUserId);
        Task<bool> sendUserMessageAsync(ClaimsPrincipal user, string groupId,string receiverId,string message);
        Task<string> getUserIdAsync(ClaimsPrincipal user);
        Task<string?> createCallAsync(CreateCallVM createCallVM,string groupId);
        Task<bool> writeAudioFileAsync(WriteCallAudioDTO writeCallAudioDTO);
        Task<bool> checkPermissionAsync(ClaimsPrincipal user, string groupId);
    }
}
