using API.Entities;
using API.Entities.Articles;

namespace API.DTOs
{
    public class ArticleAddUpdateDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public string Author { get; set; }
        public int Category { get; set; }
        public ICollection<TagBDto> Tags { get; set; }
    }
}
