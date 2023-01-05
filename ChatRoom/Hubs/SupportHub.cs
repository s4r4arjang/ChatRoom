﻿using ChatRoom.Models.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Hubs
{
    public class SupportHub : Hub
    {

        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;

        public SupportHub(IChatRoomService chatRoomService, IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
        }
        public async override Task OnConnectedAsync()
        {
            var rooms = await _chatRoomService.GetAllrooms();
            await Clients.Caller.SendAsync("GetRooms", rooms);
            await base.OnConnectedAsync();
        }


        public async Task LoadMessage(Guid roomId)
        {
            var message = await _messageService.GetChatMessage(roomId);
            await Clients.Caller.SendAsync("getNewMessage", message);
        }
    }
}
