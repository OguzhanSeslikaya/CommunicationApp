using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Abstractions
{
    public interface IUserMessageHubService
    {
        Task userSendMessageAsync(/*string message, string senderId, string receiverId*/);
    }
}
