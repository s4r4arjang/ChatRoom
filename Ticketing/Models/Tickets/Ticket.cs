using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models.Tickets
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        [DisplayFormat(HtmlEncode = true)]
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public List<TicketImage> ArticleImage { get; set; }

    }
}
