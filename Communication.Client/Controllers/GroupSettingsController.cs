using Communication.Business.Abstractions.ClientServices.GroupServices;
using Communication.Entity.ViewModels.Groups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Communication.Client.Controllers
{
    [Authorize]
    public class GroupSettingsController : Controller
    {
        private readonly IGroupSettingsService _groupSettingsService;

        public GroupSettingsController(IGroupSettingsService groupSettingsService)
        {
            _groupSettingsService = groupSettingsService;
        }

        public async Task<IActionResult> general()
        {
            return View();
        }
        public async Task<IActionResult> requestsToJoin()
        {
            return View(await _groupSettingsService.getRequestsToJoinAsync(HttpContext.Session.GetString("groupId")));
        }
        [HttpPost]
        public async Task<IActionResult> declineRequestToJoin(string requestId)
        {
            bool _succeeded = await _groupSettingsService.declineRequestToJoinAsync(requestId);
            return Ok(new { succeeded = _succeeded });
        }
        [HttpPost]
        public async Task<IActionResult> acceptRequestToJoin(string requestId)
        {
            bool _succeeded = await _groupSettingsService.acceptRequestToJoinAsync(requestId);
            return Ok(new { succeeded = _succeeded });
        }
        public async Task<IActionResult> member()
        {
            return View(await _groupSettingsService.getMembers(HttpContext.Session.GetString("groupId")));
        }
        [HttpPost]
        public async Task<IActionResult> kickMember(string userId)
        {
            return Ok(new {succeeded=_groupSettingsService.kickMember(userId,HttpContext.Session.GetString("groupId"))});
        }
        public async Task<IActionResult> calls()
        {
            await _groupSettingsService.checkVoicesAsync();

            ICollection<CallsVM> calls = await _groupSettingsService.getCalls(HttpContext.Session.GetString("groupId"));

            return View(calls);
        }
        public async Task<IActionResult> getVoice(string id)
        {
            string path = await _groupSettingsService.getVoiceFilePathById(id);
            return File(new FileStream(path,FileMode.Open),"audio/wav");
        }
    }
}
