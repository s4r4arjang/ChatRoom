using Microsoft.EntityFrameworkCore;
using Ticketing.Models.File;

namespace Ticketing.Models.Articles
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
       : base(options)
        {
        }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FileTable> Files { get; set; }
    }
}
