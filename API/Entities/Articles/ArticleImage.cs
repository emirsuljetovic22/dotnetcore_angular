using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Articles
{
    public class ArticleImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool Highlighted { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public Article Article { get; set; }
    }
}
