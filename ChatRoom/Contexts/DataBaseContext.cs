using ChatRoom.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Contexts
{

    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Chatroom> ChatRooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
