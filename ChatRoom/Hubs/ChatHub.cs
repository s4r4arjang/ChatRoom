using ChatRoom.Models.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    {

        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;
        public ChatHub(IChatRoomService chatRoomService, IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
        }

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        //public Task SendPrivateMessage(string user, string message)
        //{
        //    return Clients.User(user).SendAsync("ReceivePVMessage", message);
        //}


        public async Task SendNewMessage(string Sender, string Message)
        {
            var roomId = await _chatRoomService.GetChatRoomForConnection(Context.ConnectionId);

            MessageDto messageDto = new MessageDto()
            {
                Message = Message,
                Sender = Sender,
                Time = DateTime.Now,
            };

            await _messageService.SaveChatMessage(roomId, messageDto);
            await Clients.Groups(roomId.ToString())
                .SendAsync("getNewMessage", messageDto.Sender, messageDto.Message, messageDto.Time.ToShortDateString());
        }


        public override async Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                await base.OnConnectedAsync();
                return;
            }
            var roomId = await _chatRoomService.CreateChatRoom(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            await Clients.Caller.
                SendAsync("getNewMessage", "پشتیبانی باگتو", "سلام وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", DateTime.Now.ToShortTimeString());
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
