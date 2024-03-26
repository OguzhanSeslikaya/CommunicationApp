using Communication.Business.Abstractions.ClientServices.GroupServices;
using Communication.Business.Abstractions.Services.UserServices;
using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Abstractions.Repositories.Company;
using Communication.DataAccess.Abstractions.Repositories.Company.RequestToJoin;
using Communication.Entity.Models.Company;
using Communication.Entity.ViewModels.Groups;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.DataAccess.Abstractions.Repositories.File.CallFile;
using Communication.Entity.Models.File.LocalStorage;
using Communication.DataAccess.Abstractions.Repositories.Company.Call;
using Communication.Business.Abstractions.StorageServices;
using Communication.Entity.DTO.File;
using Communication.Entity.Models.User.Identity;

namespace Communication.Business.Concretes.ClientServices.GroupServices
{
    public class GroupSettingsService : IGroupSettingsService
    {
        private readonly IRequestToJoinGroupReadRepository _requestToJoinReadRepository;
        private readonly IRequestToJoinGroupWriteRepository _requestToJoinWriteRepository;
        private readonly IGroupUserReadRepository _groupUserReadRepository;
        private readonly IGroupUserWriteRepository _groupUserWriteRepository;
        private readonly ICallFileReadRepository _callFileReadRepository;
        private readonly ICallFileWriteRepository _callFileWriteRepository;
        private readonly ICallReadRepository _callReadRepository;
        private readonly ICallWriteRepository _callWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IUserService _userService;
        private readonly IGroupReadRepository _groupReadRepository;


        public GroupSettingsService(IUserService userService, IRequestToJoinGroupReadRepository requestToJoinReadRepository, IRequestToJoinGroupWriteRepository requestToJoinWriteRepository, IGroupUserReadRepository groupUserReadRepository, IGroupUserWriteRepository groupUserWriteRepository, ICallFileReadRepository callFileReadRepository, ICallFileWriteRepository callFileWriteRepository, ICallReadRepository callReadRepository, IStorageService storageService, ICallWriteRepository callWriteRepository, IGroupReadRepository groupReadRepository)
        {
            _requestToJoinReadRepository = requestToJoinReadRepository;
            _requestToJoinWriteRepository = requestToJoinWriteRepository;
            _groupUserReadRepository = groupUserReadRepository;
            _groupUserWriteRepository = groupUserWriteRepository;
            _callFileReadRepository = callFileReadRepository;
            _callFileWriteRepository = callFileWriteRepository;
            _callReadRepository = callReadRepository;
            _storageService = storageService;
            _callWriteRepository = callWriteRepository;
            _userService = userService;
            _groupReadRepository = groupReadRepository;
        }

        public async Task<ICollection<RequestsToJoinGroupVM>> getRequestsToJoinAsync(string groupId)
        {
            return await _requestToJoinReadRepository.getWhere(a => a.groupId == groupId && a.state == Entity.Enums.Group.RequestToJoin.pending).Select(a => new RequestsToJoinGroupVM() { id = a.id, username = a.user.UserName, date = a.createdDate.Day + "." + a.createdDate.Month + "." + a.createdDate.Year}).ToListAsync();
        }


        public async Task<ICollection<MemberSettingsVM>> getMembers(string groupId)
        {
            return await _groupUserReadRepository.getWhere(a => a.groupId == groupId).Select(a => new MemberSettingsVM() { userId=a.userId,username=a.user.UserName }).ToListAsync();
        }

        public async Task<bool> declineRequestToJoinAsync(string requestId)
        {
            RequestToJoinGroup? request = await _requestToJoinReadRepository.getByIdAsync(requestId);
            if (request != null)
            {
                request.state = Entity.Enums.Group.RequestToJoin.denied;
                bool succeeded = _requestToJoinWriteRepository.update(request);
                await _requestToJoinWriteRepository.saveAsync();
                return succeeded;
            }
            return false;
        }

        public async Task<bool> acceptRequestToJoinAsync(string requestId)
        {
            RequestToJoinGroup? request = await _requestToJoinReadRepository.getByIdAsync(requestId);
            if (request != null)
            {
                
                bool addSucceeded = await _groupUserWriteRepository.addAsync(new() { id = Guid.NewGuid().ToString(), groupId = request.groupId, userId = request.userId } );
                await _groupUserWriteRepository.saveAsync();
                if (addSucceeded)
                {
                    request.state = Entity.Enums.Group.RequestToJoin.accepted;
                    bool succeeded = _requestToJoinWriteRepository.update(request);
                    await _requestToJoinWriteRepository.saveAsync();
                    return succeeded;
                }
            }
            return false;
        }
        public async Task<bool> kickMember(string userId, string groupId)
        {
            bool succeeded = _groupUserWriteRepository.remove(await _groupUserReadRepository.getWhere(a => a.userId==userId && a.groupId == groupId).FirstOrDefaultAsync());
            await _groupUserWriteRepository.saveAsync();
            return succeeded;
        }

        public async Task checkVoicesAsync()
        {
            var callList = await _callReadRepository.getAll().Include(a => a.group).ToListAsync();
            foreach (var call in callList)
            {
                List<CallFile> callFiles = await _callFileReadRepository.getWhere(a => a.callId==call.id&&a.isMixed==false).Take(2).ToListAsync();
                if(callFiles.Count > 1)
                {
                    UploadFileDTO? file = await _storageService.mixTwoAudioFiles(callFiles[0].path, callFiles[1].path, $"Files\\MixedCalls\\{call.group.name}");
                    if (file != null)
                    {
                        callFiles[0].isMixed = true;
                        callFiles[1].isMixed = true;
                        _callFileWriteRepository.update(callFiles[0]);
                        _callFileWriteRepository.update(callFiles[1]);
                        CallFile callFile = new CallFile()
                        {
                            id = Guid.NewGuid().ToString(),
                            callId = call.id,
                            isMixed = true,
                            fileName = file.fileName,
                            path = file.path,
                            storage = _storageService.storageName
                        };
                        bool isAdded = await _callFileWriteRepository.addAsync(callFile);

                        await _callFileWriteRepository.saveAsync();



                        if (call != null && isAdded)
                        {
                            call.mainCallFile = callFile;
                            call.mainCallFileId = callFile.id;
                            _callWriteRepository.update(call);
                            await _callWriteRepository.saveAsync();
                        }
                    }
                    
                    
                    
                }
            }
        }

        public async Task<string> getVoiceFilePathById(string id)
        {
            return (await _callFileReadRepository.getByIdAsync(id)).path;
        }

        public async Task<ICollection<CallsVM>> getCalls(string groupId)
        {
            ICollection<CallsVM> calls = await _callReadRepository.getWhere(a => a.groupId==groupId&&a.mainCallFile != null).Select(a => new CallsVM()
            {
                calledName=a.called.UserName,
                callerName=a.caller.UserName,
                callFileId=a.mainCallFileId,
                date=a.createdDate
            }).ToListAsync();
            
            return calls;
        }

        
    }
}
