using Communication.Business.Abstractions.Services.UserServices;
using Communication.DataAccess.Abstractions.Repositories.Company;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.Entity.Models.Company;
using Communication.Entity.Models.User.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business.Concretes.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGroupUserReadRepository _groupUserRoleReadRepository;

        public UserService(UserManager<AppUser> userManager, IGroupUserReadRepository groupUserRoleReadRepository)
        {
            _userManager = userManager;
            _groupUserRoleReadRepository = groupUserRoleReadRepository;
        }

        public async Task<AppUser?> getUserById(string id)
        {
            var _user = await _userManager.FindByIdAsync(id);
            return _user;
        }

        public async Task<AppUser?> getUserByToken(ClaimsPrincipal user)
        {
            var _user = await _userManager.GetUserAsync(user);
            return _user;
        }

        public ICollection<Group> getUserGroups(AppUser user)
        {
            return _groupUserRoleReadRepository.getWhere(g => g.user.Id == user.Id).Select(g => new Communication.Entity.Models.Company.Group() { id = g.groupId , name = g.group.name}).ToList();
        }
    }
}
