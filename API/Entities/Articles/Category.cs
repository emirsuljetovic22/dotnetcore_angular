using CloudinaryDotNet.Actions;
using Newtonsoft.Json;

namespace API.Entities.Articles
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        [JsonIgnore]
        public ICollection<Article> Articles { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
    }
}
