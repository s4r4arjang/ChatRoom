namespace Ticketing.Models.Articles
{
    public class Category
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public bool IsActive { get; private set; }
        public List<Article> Articles { get; private set; }
    }
}
