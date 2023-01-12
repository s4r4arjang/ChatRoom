using Microsoft.EntityFrameworkCore;

namespace Ticketing.Models.Tickets
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
       : base(options)
        {
        }


        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketImage> TicketImages { get; set; }
    }
}

