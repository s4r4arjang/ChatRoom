using System.ComponentModel.DataAnnotations;
using Ticketing.Models.File;

namespace Ticketing.Models.Articles
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        [DisplayFormat(HtmlEncode = true)]
        public string Content { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
       
        public List<FileTable> FileList { get; set; }
        public string ArticleImage { get; set; }

    }
}
