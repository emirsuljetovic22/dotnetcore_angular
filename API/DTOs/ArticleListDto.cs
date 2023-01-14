
using API.Entities.Articles;

namespace API.DTOs
{
    public class ArticleListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<ArticleImage> ArticleImages { get; set; } = new List<ArticleImage>();
    }
}
 