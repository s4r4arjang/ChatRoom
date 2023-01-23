using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models.Tickets
{
    public class CreateTicketVewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]

        public string Title { get; set; }
        [Display(Name = "کلیات")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string ShortDescription { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DisplayFormat(HtmlEncode = true)]
        public string Content { get; set; }
        public List<IFormFile> TicketFile { get; set; }
    }
}
