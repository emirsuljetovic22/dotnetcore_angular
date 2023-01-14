using Newtonsoft.Json;

namespace API.Entities.Articles
{
    public class Tag
    {
        public int Id { get; set; }
        public string ArticleTagName { get; set; }
        [JsonIgnore]
        public ICollection<Article> Articles { get; set; }
    }
}
