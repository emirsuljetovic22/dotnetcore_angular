using API.Entities.Articles;

namespace API.DTOs
{
    public class CategoryDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
