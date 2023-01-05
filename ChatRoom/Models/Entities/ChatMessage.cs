namespace ChatRoom.Models.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public Chatroom Chatroom { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}
