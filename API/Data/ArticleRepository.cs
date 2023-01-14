using API.DTOs;
using API.Entities.Articles;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public ArticleRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Article> GetArticleByUrl(string url)
        {
            return await _context.Articles
                .Include(ac => ac.ArticleCategories)
                .Include(t => t.Tags)
                .SingleOrDefaultAsync(x => x.Url == url);
        }

        public async Task<Article> GetArticleById(int id)
        {
            return await _context.Articles
                .Include(ac => ac.ArticleCategories)
                .Include(t => t.Tags)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<ArticleListDto>> GetArticlesAsync(PaginationParams paginationParams)
        {
            var articles = _context.Articles
                .Include(ai => ai.ArticleImages)
                .Include(a => a.ArticleCategories)
                .Include(t => t.Tags)
                .AsQueryable();

            return await PagedList<ArticleListDto>.CreateAsync(articles.ProjectTo<ArticleListDto>(_mapper.ConfigurationProvider)
                .AsNoTracking(), paginationParams.PageNumber, paginationParams.PageSize);
        }
         
        public void AddArticle(Article article)
        {
            _context.Articles.Add(article);
        }
        public void AddArticleCategory(Category category)
        {
            _context.Categories.Add(category);
        }
        public void UpdateArticle(Article article)
        {
            _context.Entry(article).State = EntityState.Modified;
        } 

        public async Task<bool> UrlExists(string url)
        {
            if (await _context.Articles.AnyAsync(x => x.Url == url))
                return true;

            return false;
        }

        public async Task DeleteArticle(string url)
        {
            var article = await GetArticleByUrl(url);
            _context.Articles.Remove(article);
        }

        public void AddImage(ArticleImage articleImage)
        {
            _context.ArticleImages.Add(articleImage);
        }
    }
}
