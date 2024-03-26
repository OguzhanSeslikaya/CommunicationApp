using Communication.Business.Abstractions.ClientServices.GroupServices;
using Communication.Business.Abstractions.Services.UserServices;
using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.Entity.Models.Company;
using Communication.Entity.Models.User.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Concretes.ClientServices.GroupServices
{
    public class BaseGroupService : IBaseGroupService
    {
        private readonly IGroupReadRepository _groupReadRepository;
        private readonly IUserService _userService;
        private readonly IGroupUserReadRepository _groupUserReadRepository;

        public BaseGroupService(IGroupReadRepository groupReadRepository, IUserService userService, IGroupUserReadRepository groupUserReadRepository)
        {
            _groupReadRepository = groupReadRepository;
            _userService = userService;
            _groupUserReadRepository = groupUserReadRepository;
        }

        public async Task<Group?> getGroupById(string id)
        {
            return await _groupReadRepository.getByIdAsync(id);
        }

        public async Task<Group?> getGroupByTokenWithId(ClaimsPrincipal user,string id)
        {
            AppUser? _user = await _userService.getUserByToken(user);
            if(_user != null) {
                return await _groupUserReadRepository.getWhere(a => a.groupId==id&&a.userId==_user.Id).Include(a => a.group).Select(a => a.group).FirstOrDefaultAsync();
            }
            return null;
        }
        public async Task<Group?> getGroupByTokenWithId(AppUser user, string id)
        { 
                return await _groupUserReadRepository.getWhere(a => a.groupId == id && a.userId == user.Id).Include(a => a.group).Select(a => a.group).FirstOrDefaultAsync();
        }

    }
}
