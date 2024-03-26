using Communication.Business.Abstractions.Services.UserServices;
using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUserMessage;
using Communication.Entity.DTO.SignalR;
using Communication.Entity.ViewModels.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    [Authorize]
    public class UserMessageHub : Hub
    {
        private readonly IGroupUserMessageWriteRepository _userMessageWriteRepository;
        private readonly IGroupReadRepository _groupReadRepository;
        private readonly IUserService _userService;

        public UserMessageHub(IGroupUserMessageWriteRepository userMessageWriteRepository, IGroupReadRepository groupReadRepository, IUserService userService)
        {
            _userMessageWriteRepository = userMessageWriteRepository;
            _groupReadRepository = groupReadRepository;
            _userService = userService;
        }

        public async Task userSendMessageAsync(SignalRMessageDTO message)
        {
            await _userMessageWriteRepository.addAsync(new Communication.Entity.Models.Company.GroupUserMessage() { id = Guid.NewGuid().ToString(), content = message.message, receiverId = message.receiverId, senderId = message.senderId, groupId = message.groupId});
            await _userMessageWriteRepository.saveAsync();
            await Clients.User(message.receiverId).SendAsync("message",message);
        }

        public async Task userSendVoiceCallRequest(SignalRVoiceCallRequestDTO voiceCallRequest)
        {
            await Clients.User(voiceCallRequest.receiverId).SendAsync("voiceCallRequest", new VoiceCallRequestVM()
            {
                groupId = voiceCallRequest.groupId,
                groupName = (await _groupReadRepository.getByIdAsync(voiceCallRequest.groupId)).name,
                senderName = (await _userService.getUserById(voiceCallRequest.senderId)).UserName,
                senderId = voiceCallRequest.senderId,
                receiverId = voiceCallRequest.receiverId
            });
        }

        public async Task userSendVoiceCallReceive(SignalRVoiceCallReceiveDTO voiceCallReceive)
        {
            await Clients.User(voiceCallReceive.receiverId).SendAsync("voiceCallReceive", voiceCallReceive);
        }

        public async Task sendVoice(SignalRSendVoiceDTO stream)
        {
            await Clients.User(stream.receiverId).SendAsync("voiceCall", stream);
        }

        public async Task endCallRequest(string receiverId)
        {
            Console.WriteLine("cagri sonlandrild . "+ receiverId);
            await Clients.User(receiverId).SendAsync("endCall");
        }

    }
}
