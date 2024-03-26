using Communication.Business.Abstractions.Services.GroupServices;
using Communication.Business.Abstractions.Services.UserServices;
using Communication.Business.Concretes.ClientServices.GroupServices;
using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Abstractions.Repositories.Company;
using Communication.DataAccess.Abstractions.Repositories.Company.RequestToJoin;
using Communication.Entity.DTO.Groups;
using Communication.Entity.Enums.Group;
using Communication.Entity.Models.Company;
using Communication.Entity.Models.User.Identity;
using Communication.Entity.ViewModels.Groups;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;

namespace Communication.Business.Concretes.Services.GroupServices
{
    public class HomeGroupService : IHomeGroupService
    {
        private readonly IGroupUserReadRepository _groupUserReadRepository;
        private readonly IGroupUserWriteRepository _groupUserWriteRepository;
        private readonly IUserService _userService;
        private readonly IGroupReadRepository _groupReadRepository;
        private readonly IRequestToJoinGroupReadRepository _requestToJoinGroupReadRepository;
        private readonly IRequestToJoinGroupWriteRepository _requestToJoinGroupWriteRepository;

        public HomeGroupService(IGroupUserReadRepository groupUserReadRepository, IGroupUserWriteRepository groupUserWriteRepository, IUserService userService, IGroupReadRepository groupReadRepository, IRequestToJoinGroupReadRepository requestToJoinGroupReadRepository, IRequestToJoinGroupWriteRepository requestToJoinGroupWriteRepository)
        {
            _groupUserReadRepository = groupUserReadRepository;
            _groupUserWriteRepository = groupUserWriteRepository;
            _userService = userService;
            _groupReadRepository = groupReadRepository;
            _requestToJoinGroupReadRepository = requestToJoinGroupReadRepository;
            _requestToJoinGroupWriteRepository = requestToJoinGroupWriteRepository;
        }

        public async Task<MyGroupVM?> createGroupAsync(string groupName,ClaimsPrincipal user)
        {
            Entity.Models.Company.Group? _group = await _groupReadRepository.getWhere(a => a.name == groupName).FirstOrDefaultAsync();
            if (_group != null)
            {
                return null;
            }
            var _user = await _userService.getUserByToken(user);
            if (_user != null)
            {
                
                Entity.Models.Company.Group group = new Entity.Models.Company.Group() {id = Guid.NewGuid().ToString(),  name = groupName , creator=_user,creatorId=_user.Id};

                    GroupUser groupUser = new() {id=Guid.NewGuid().ToString() , group = group,user= _user};
                bool three = await _groupUserWriteRepository.addAsync(groupUser);

                if (three) { 
                    await _groupUserWriteRepository.saveAsync();
                    return new MyGroupVM() { id = group.id, groupName = group.name};
                }

            }
            return null;
        }

        public async Task<ICollection<MyGroupVM>> getUserGroupsAsync(ClaimsPrincipal user)
        {
            var _user = await _userService.getUserByToken(user);
            if (_user != null)
            {
                var userGroups = _userService.getUserGroups(_user);
                if (userGroups != null)
                {
                    return userGroups.Select(g => new MyGroupVM() { id = g.id, groupName=g.name, myRole = "Admin", groupMemberCount = _groupUserReadRepository.table.Count(a => a.groupId == g.id) }).ToList();
                }

            }
                return new List<MyGroupVM>();
        }

        public async Task<GroupVM> groupsAndUserRequests(ClaimsPrincipal user)
        {
            var _user = await _userService.getUserByToken(user);
            GroupVM groupVM = new GroupVM();
            if (_user != null)
            {
                List<GroupUser> myGroups = await _groupUserReadRepository.getWhere(a => a.userId == _user.Id).ToListAsync();
                
                List<GroupDTO> groupList = await _groupReadRepository.table.Select(a => new GroupDTO() { id = a.id, groupName = a.name, numberOfMembers = _groupUserReadRepository.table.Where(b => b.groupId == a.id).Count()}).ToListAsync();

                foreach (var item in myGroups)
                {
                    foreach (var item1 in groupList)
                    {
                        if(item.groupId == item1.id)
                        {
                            groupList.Remove(item1);
                            break;
                        }
                    }
                }

                groupVM.groups = groupList;

                groupVM.requestsToJoin = await _requestToJoinGroupReadRepository.getWhere(a => a.userId==_user.Id).Select(a => new RequestToJoinDTO() { id =a.id, groupName=a.group.name, state = Enum.GetName<RequestToJoin>(a.state)}).ToListAsync();
            }
            return groupVM;
        }

        public async Task<bool> leaveGroupAsync(MyGroupVM myGroupVM,ClaimsPrincipal user)
        {
            var _user = await _userService.getUserByToken(user);
            if (_user != null)
            {
                GroupUser? groupUserRole = await _groupUserReadRepository.getWhere(a => a.groupId==myGroupVM.id && a.userId==_user.Id).FirstOrDefaultAsync();
                if (groupUserRole != null)
                {
                    bool remove = _groupUserWriteRepository.remove(groupUserRole);
                    if (remove)
                    {
                        await _groupUserWriteRepository.saveAsync();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> removeRequest(ClaimsPrincipal user, string requestId)
        {
            AppUser? _user = await _userService.getUserByToken(user);
            if (_user != null)
            {
                RequestToJoinGroup? requestToJoin = await _requestToJoinGroupReadRepository.getWhere(a => a.userId == _user.Id && a.id == requestId).FirstOrDefaultAsync();
                if (requestToJoin == null)
                {
                    return false;
                }
                bool succeeded = (await _requestToJoinGroupWriteRepository.removeAsync(requestId));
                await _requestToJoinGroupWriteRepository.saveAsync();
                return succeeded;
            }
            return false;
        }

        public async Task<bool> RequestToJoinGroup(ClaimsPrincipal user, string groupId)
        {
            AppUser? _user = await _userService.getUserByToken(user);
            if(_user != null)
            {
                RequestToJoinGroup? requestToJoin = await _requestToJoinGroupReadRepository.getWhere(a => a.userId == _user.Id && a.groupId == groupId).FirstOrDefaultAsync();
                if(requestToJoin != null)
                {
                    return false;
                }
                requestToJoin = new RequestToJoinGroup() { id = Guid.NewGuid().ToString(), userId=_user.Id, groupId = groupId,state = RequestToJoin.pending};
                bool succeeded = await _requestToJoinGroupWriteRepository.addAsync(requestToJoin);
                await _requestToJoinGroupWriteRepository.saveAsync();
                return succeeded;
            }
            return false;
        }
    }
}
