using Microsoft.AspNetCore.SignalR;
using SignalR.Abstractions;
using SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.HubServices
{

    public class UserMessageHubService : IUserMessageHubService
    {
        readonly IHubContext<UserMessageHub> _hubContext;

        public UserMessageHubService(IHubContext<UserMessageHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task userSendMessageAsync(/*string message,string senderId ,string receiverId*/)
        {
            //return task.completedtask;
            await _hubContext.Clients.All.SendAsync("deneme", "asd");
        }
    }
}
