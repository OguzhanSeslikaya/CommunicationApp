using Communication.Business.Abstractions.Services.GroupServices;
using Communication.Entity.Models.User.Identity;
using Communication.Entity.ViewModels.Groups;
using Communication.Entity.ViewModels.User;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Communication.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHomeGroupService _homeGroupService;
        private readonly IValidator<CreateGroupVM> _validator;

        public HomeController(IHomeGroupService homeGroupService, IValidator<CreateGroupVM> validator)
        {
            _homeGroupService = homeGroupService;
            _validator = validator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> groups()
        {
            GroupVM groupVM = await _homeGroupService.groupsAndUserRequests(User);
            return View(groupVM);
        }

        public async Task<IActionResult> myGroups()
        {
            ICollection<MyGroupVM> myGroupsVM = await _homeGroupService.getUserGroupsAsync(User);
            return View(myGroupsVM);
        }

        [HttpPost]
        public async Task<IActionResult> createGroup([FromForm]CreateGroupVM createGroupVM)
        {
            string errorMessage = "";
            ValidationResult validationResult = await _validator.ValidateAsync(createGroupVM);
            if (!validationResult.IsValid)
            {
                foreach (ValidationFailure error in validationResult.Errors)
                {
                    errorMessage += $"* {error.ErrorMessage} <br>";
                }

                return Ok(new { succeeded = false , message = errorMessage });
            }
            MyGroupVM? _group = await _homeGroupService.createGroupAsync(createGroupVM.groupName, User);
            if (_group != null)
            {
                return Ok(new { succeeded = true, group = _group, message = "* Group was successfully created." });
            }
            return Ok(new {succeeded=false, message = "* Group name already taken." });
            
        }

        [HttpPost]
        public async Task<IActionResult> RequestToJoinGroup(string groupId)
        {
            return Ok(new {succeeded = (await _homeGroupService.RequestToJoinGroup(User, groupId)) });
        }

        [HttpPost]
        public async Task<IActionResult> leaveGroup([FromForm]MyGroupVM myGroupVM)
        {
            return Ok(new {succeeded=(await _homeGroupService.leaveGroupAsync(myGroupVM,User))});
        }

        [HttpPost]
        public async Task<IActionResult> removeRequest(string requestId)
        {
            return Ok(new { succeeded = (await _homeGroupService.removeRequest(User, requestId)) });
        }

       
    }
}