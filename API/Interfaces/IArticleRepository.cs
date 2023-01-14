using API.DTOs;
using API.Entities.Articles;
using API.Helpers;

namespace API.Interfaces
{
    public interface IArticleRepository
    {
        void UpdateArticle(Article article);
        Task<PagedList<ArticleListDto>> GetArticlesAsync(PaginationParams paginationParams);
        Task<Article> GetArticleByUrl(string url);
        Task<Article> GetArticleById(int id);
        void AddArticle(Article article);
        void AddImage(ArticleImage articleImage);
        Task<bool> UrlExists(string url);
        Task DeleteArticle(string url);
    }
}
