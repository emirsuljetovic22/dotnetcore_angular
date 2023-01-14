using API.DTOs;
using API.Entities.Articles;

namespace API.Interfaces
{
    public interface ICategoryRepository
    {
        void UpdateCategory(Category category);
        Task<List<CategoryToReturnDto>> GetAll();
        Task<Category> GetCategoryById(int id);
        void AddCategory(Category category);
        Task DeleteCategory(int id);
        Task<bool> CategoryNameExists(string categoryName, int categoryId);
    }
}
