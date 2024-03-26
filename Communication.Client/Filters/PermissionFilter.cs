using Communication.Business.Abstractions.ClientServices.GroupServices;
using Communication.Business.Abstractions.Services.GroupServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Communication.Client.Filters
{
    public class PermissionFilter : IAsyncActionFilter
    {
        private readonly IGroupService _groupService;

        public PermissionFilter(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            string? controller = descriptor.RouteValues["controller"];

            if (controller == null)
            {
                await next();
                return;
            }
            if (controller == "GroupSettings")
            {
                bool hasPermission = await _groupService.checkPermissionAsync(context.HttpContext.User, context.HttpContext.Session.GetString("groupId"));
                if (!hasPermission)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }
            }
            await next();
        }
    }
}
