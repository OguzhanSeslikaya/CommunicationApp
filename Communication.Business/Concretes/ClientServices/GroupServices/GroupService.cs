using Communication.Business.Abstractions.ClientServices.GroupServices;
using Communication.Business.Abstractions.Services.GroupServices;
using Communication.Business.Abstractions.Services.UserServices;
using Communication.Business.Abstractions.StorageServices;
using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUserMessage;
using Communication.DataAccess.Abstractions.Repositories.Company;
using Communication.DataAccess.Abstractions.Repositories.File.PostFile;
using Communication.DataAccess.Abstractions.Repositories.User.UserPosts;
using Communication.Entity.DTO.File;
using Communication.Entity.DTO.Groups;
using Communication.Entity.DTO.Messages;
using Communication.Entity.Models.Company;
using Communication.Entity.Models.File.LocalStorage;
using Communication.Entity.Models.User.Identity;
using Communication.Entity.Models.User.UserPosts;
using Communication.Entity.ViewModels.Groups;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.DataAccess.Concretes.Repositories.Company.GroupUser;
using Communication.DataAccess.Abstractions.Repositories.Company.Call;
using Communication.DataAccess.Abstractions.Repositories.File.CallFile;

namespace Communication.Business.Concretes.Services.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly IUserService _userService;
        private readonly IGroupUserReadRepository _groupUserReadRepository;
        private readonly IStorageService _storageService;
        private readonly IUserPostWriteRepository _userPostWriteRepository;
        private readonly IBaseGroupService _baseGroupService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserPostReadRepository _userPostReadRepository;
        private readonly IGroupReadRepository _groupReadRepository;
        private readonly IGroupUserMessageReadRepository _groupUserMessageReadRepository;
        private readonly IGroupUserMessageWriteRepository _groupUserMessageWriteRepository;
        private readonly ICallReadRepository _callReadRepository;
        private readonly ICallWriteRepository _callWriteRepository;
        private readonly ICallFileReadRepository _callFileReadRepository;
        private readonly ICallFileWriteRepository _callFileWriteRepository;
        public GroupService(IUserService userService, IGroupUserReadRepository groupUserReadRepository, IStorageService storageService, IPostFileWriteRepository postFileWriteRepository, IUserPostWriteRepository userPostWriteRepository, IBaseGroupService baseGroupService, UserManager<AppUser> userManager, IUserPostReadRepository userPostReadRepository, IGroupReadRepository groupReadRepository, IGroupUserMessageReadRepository groupUserMessageReadRepository, IGroupUserMessageWriteRepository groupUserMessageWriteRepository, ICallReadRepository callReadRepository, ICallWriteRepository callWriteRepository, ICallFileReadRepository callFileReadRepository, ICallFileWriteRepository callFileWriteRepository)
        {
            _userService = userService;
            _groupUserReadRepository = groupUserReadRepository;
            _storageService = storageService;
            _userPostWriteRepository = userPostWriteRepository;
            _baseGroupService = baseGroupService;
            _userManager = userManager;
            _userPostReadRepository = userPostReadRepository;
            _groupReadRepository = groupReadRepository;
            _groupUserMessageReadRepository = groupUserMessageReadRepository;
            _groupUserMessageWriteRepository = groupUserMessageWriteRepository;
            _callReadRepository = callReadRepository;
            _callWriteRepository = callWriteRepository;
            _callFileReadRepository = callFileReadRepository;
            _callFileWriteRepository = callFileWriteRepository;
        }
        public async Task<ICollection<GroupUserDTO>> getAllUsers(string groupId, ClaimsPrincipal user)
        {
            var _user = await _userService.getUserByToken(user);
            if(_user != null)
            {
                return _groupUserReadRepository.getWhere(a => a.groupId == groupId).Select(a => new GroupUserDTO() { id = a.userId, username = a.user.UserName }).ToHashSet(); ;
                   
            }
            return new List<GroupUserDTO>();
        }

        public async Task<bool> createPostAsync(CreatePostVM createPostVM,ClaimsPrincipal user)
        {
                AppUser? appUser = await _userService.getUserByToken(user);
                if (appUser != null)
                {
                    Group? group = await _baseGroupService.getGroupByTokenWithId(appUser, createPostVM.groupId);
                    if (group != null)
                    {
                        if(createPostVM.file != null) 
                        { 
                        UploadFileDTO? file = await _storageService.uploadAsync($"Files\\Groups\\{group.name}", createPostVM.file);
                            if (file != null)
                            {
                                PostFile postFile = new() { id = Guid.NewGuid().ToString(), fileName = file.fileName, path = file.path, storage = _storageService.storageName };

                                bool isAdd = await _userPostWriteRepository.addAsync(new Entity.Models.User.UserPosts.UserPost()
                                {
                                    id = Guid.NewGuid().ToString(),
                                    content = createPostVM.content,
                                    user = appUser,
                                    groupId = group.id,
                                    postFile = postFile
                                });
                                await _userPostWriteRepository.saveAsync();
                                return isAdd;
                            }
                        }
                        else
                        {
                            bool isAdd = await _userPostWriteRepository.addAsync(new Entity.Models.User.UserPosts.UserPost()
                            {
                                id = Guid.NewGuid().ToString(),
                                content = createPostVM.content,
                                user = appUser,
                                groupId = group.id,
                                postFile = null
                            });
                            await _userPostWriteRepository.saveAsync();
                            return isAdd;
                    }
                    }
                }
            return false;
        }

        public async Task<ICollection<PostDTO>?> getAllUserPostsInGroup(ClaimsPrincipal user, string groupId)
        {

            if ((await _baseGroupService.getGroupByTokenWithId(user, groupId)) != null)
            {
                return _userPostReadRepository.getWhere(a => a.groupId == groupId).OrderByDescending(a => a.createdDate).Select(a => new PostDTO { id = a.id, content = a.content, user = new() { id = a.userId, role = "yok", username = a.user.UserName},fileId=a.postFileId,fileName=a.postFile.fileName,createdDate=a.createdDate.Day + "." + a.createdDate.Month + "." + a.createdDate.Year }).ToHashSet();
            }
            return null;
        }

        public async Task<string?> getGroupNameByGroupId(ClaimsPrincipal user , string groupId)
        {
            AppUser _user = await _userService.getUserByToken(user);
            if(_user != null)
            {
                return await _groupUserReadRepository.getWhere(a => a.groupId == groupId && a.userId == _user.Id).Select(a => a.group.name).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<string?> getPostFileNameByPostFileId(string fileId)
        {
            return await _userPostReadRepository.getWhere(a => a.postFileId==fileId).Select(a => a.postFile.fileName).FirstOrDefaultAsync();
        }

        public async Task<UserMessagesDTO?> getUserMessagesAsync(ClaimsPrincipal user,string groupId,string targetUserId)
        {
            var _user = await _userService.getUserByToken(user);
            var _targetUser = await _userService.getUserById(targetUserId);
            if (_user == null || _targetUser == null) return null;
            ICollection<MessageDTO> messages = await _groupUserMessageReadRepository.getWhere(a => ((a.receiverId == targetUserId && _user.Id == a.senderId) || (a.receiverId == _user.Id && a.senderId == targetUserId))&&a.groupId==groupId).Select(a => new MessageDTO() { didISend = a.senderId==_user.Id, message= a.content, sendedDate = a.createdDate.ToShortDateString()}).ToListAsync();
            return new() { messages = messages, targetUserId=targetUserId, targetUserName=_targetUser.UserName };
        }

        public async Task<bool> sendUserMessageAsync(ClaimsPrincipal user, string groupId, string receiverId, string message)
        {
            var _user = await _userService.getUserByToken(user);
            var _receiver = await _userService.getUserById(receiverId);
            if (_user == null || _receiver == null){ return false; }
            var group = await _groupUserReadRepository.getWhere(a => a.groupId == groupId || a.userId == _user.Id).FirstOrDefaultAsync();
            if (group == null) {  return false; }
            bool succeeded = await _groupUserMessageWriteRepository.addAsync(new GroupUserMessage() { id = Guid.NewGuid().ToString(), content = message,groupId=groupId,receiverId=receiverId, senderId=_user.Id});
            await _groupUserMessageWriteRepository.saveAsync();
            return succeeded;
        }

        public async Task<string> getUserIdAsync(ClaimsPrincipal user)
        {
            return (await _userService.getUserByToken(user)).Id;
        }

        public async Task<string?> createCallAsync(CreateCallVM createCallVM, string groupId)
        {
            string callId = Guid.NewGuid().ToString();
            bool succeeded = await _callWriteRepository.addAsync(new Call() { id = callId, calledId = createCallVM.calledId, callerId = createCallVM.callerId, groupId = groupId});
            await _callWriteRepository.saveAsync();
            if (succeeded)
            {
                return callId;
            }
            return null;
        }

        public async Task<bool> writeAudioFileAsync(WriteCallAudioDTO writeCallAudioDTO)
        {
            string base64Data = writeCallAudioDTO.data.Split(',')[1];
            
            byte[] fileBytes = Convert.FromBase64String(base64Data);

            Group? group = await _groupReadRepository.getWhere(a => a.id == writeCallAudioDTO.groupId).FirstOrDefaultAsync();
            if (group != null)
            {
                UploadFileDTO? file = await _storageService.uploadWithBytesAsync($"Files\\Calls\\{group.name}", fileBytes);
                if (file != null)
                {
                    CallFile callFile = new() { id = Guid.NewGuid().ToString(), fileName = file.fileName, path = file.path, storage = _storageService.storageName, callId = writeCallAudioDTO.callId };

                    bool isAdd = await _callFileWriteRepository.addAsync(callFile);
                    await _callFileWriteRepository.saveAsync();
                    return isAdd;
                }

            }

            return false;
        }

        public async Task<bool> checkPermissionAsync(ClaimsPrincipal user, string groupId)
        {
            AppUser? _user = await _userService.getUserByToken(user);
            if (_user == null)
            {
                return false;
            }

            Entity.Models.Company.Group? group = await _groupReadRepository.getWhere(a => a.id == groupId && a.creatorId == _user.Id).FirstOrDefaultAsync();

            if (group == null)
            {
                return false;
            }

            return true;
        }

    }

    
}