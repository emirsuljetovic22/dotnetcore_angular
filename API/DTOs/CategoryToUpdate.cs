using API.Entities.Articles;
using Newtonsoft.Json;

namespace API.DTOs
{
    public class CategoryToUpdateDto
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
