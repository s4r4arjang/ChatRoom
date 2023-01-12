namespace Ticketing.Models.Tickets
{
    public class TicketImage
    {
        public int Id { get; set; } 
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public string Image { get; set; }
    }
}
