using Communication.Business.Abstractions.Services.GroupServices;
using Communication.Business.Abstractions.StorageServices;
using Communication.Entity.DTO.Groups;
using Communication.Entity.DTO.Messages;
using Communication.Entity.ViewModels.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO.Compression;
using System.Text;
using static NpgsqlTypes.NpgsqlTsVector;

namespace Communication.Client.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IWebHostEnvironment _environment;

        public GroupController(IGroupService groupService, IWebHostEnvironment environment)
        {
            _groupService = groupService;
            _environment = environment;
        }

        [HttpPost]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> createPost(CreatePostVM createPostVM)
        {
            if(HttpContext.Session.GetString("groupId") != null)
            {
                createPostVM.groupId = HttpContext.Session.GetString("groupId");
                await _groupService.createPostAsync(createPostVM, User);
                return Ok();
            }
            return Ok();
            
        }

        public async Task<IActionResult> inGroup()
        {
            var userId = await _groupService.getUserIdAsync(User);
            var groupUsers = await _groupService.getAllUsers(HttpContext.Session.GetString("groupId"), User);
            var postUsers = await _groupService.getAllUserPostsInGroup(User,HttpContext.Session.GetString("groupId"));
            bool _isAuthority = await _groupService.checkPermissionAsync(User,HttpContext.Session.GetString("groupId"));
            return View(new InGroupVM() { groupUsers = groupUsers , postUsers = postUsers , userId = userId , isAuthority =_isAuthority});
            
        }

        [HttpPost]
        public IActionResult goGroup([FromForm] string id)
        {
            HttpContext.Session.SetString("groupId", id);
            return Ok(new {succeeded=true});
        }

        public async Task<IActionResult> downloadPostFile(string fileId)
        {
            var filepath = Path.Combine(_environment.WebRootPath, $"Files/Groups/{await _groupService.getGroupNameByGroupId(User, HttpContext.Session.GetString("groupId"))}", await _groupService.getPostFileNameByPostFileId(fileId));
            return File(System.IO.File.ReadAllBytes(filepath), "application/octet-stream", System.IO.Path.GetFileName(filepath));
        }

        [HttpPost]
        public async Task<IActionResult> sendUserMessage(string receiverId,string message)
        {
            return Ok(new {succeeded = await _groupService.sendUserMessageAsync(User,HttpContext.Session.GetString("groupId"),receiverId,message)});
        }

        [HttpPost]
        public async Task<IActionResult> getUserMessages(string targetUserId)
        {
            return Ok(new {messages = await _groupService.getUserMessagesAsync(User,HttpContext.Session.GetString("groupId"),targetUserId)});
        }

        [HttpPost]
        public async Task<IActionResult> createCall(CreateCallVM createCallVM)
        {
            string? call = await _groupService.createCallAsync(createCallVM,HttpContext.Session.GetString("groupId"));
            if (call != null)
                return Ok(new {succeeded = true, callId = call });
            
            return Ok(new {succeeded = false});
        }

        [HttpPost]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> postVoiceBytes(WriteCallAudioDTO writeCallAudioDTO)
        {

            bool succeededWrite = await _groupService.writeAudioFileAsync(writeCallAudioDTO);

            return Ok(new { succeeded = succeededWrite });
        }


    }
}
