using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Articles
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public string Author { get; set; }
        public string Status { get; set; }
        public ICollection<ArticleImage> ArticleImages { get; set; } = new List<ArticleImage>();
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Category> ArticleCategories { get; set; }
        [NotMapped]
        public int CategoryId { get; set; }
        
    }
}
