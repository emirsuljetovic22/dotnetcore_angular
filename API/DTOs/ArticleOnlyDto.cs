using API.Entities;
using API.Entities.Articles;
using Newtonsoft.Json;

namespace API.DTOs
{
    public class ArticleOnlyDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Author { get; set; }
    }
}
