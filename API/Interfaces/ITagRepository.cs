using API.DTOs;
using API.Entities.Articles;
using API.Helpers;

namespace API.Interfaces
{
    public interface ITagRepository
    {
        void UpdateTag(Tag tag);
        Task<List<TagDto>> GetTagsForArticleAsync(int articleId);
        Task<List<TagDto>> GetAllTags();
        Task<Tag> GetTagById(int id);
        Task<Tag> GetTagByName(string tagName);
        void AddTag(Tag tag);
        Task DeleteTag(int id);
    }
}
