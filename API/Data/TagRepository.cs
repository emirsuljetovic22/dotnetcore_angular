using API.DTOs;
using API.Entities.Articles;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TagRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
        }

        public async Task<List<TagDto>> GetAllTags()
        {
            return await _context.Tags
                .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Tag> GetTagById(int id)
        {
            return await _context.Tags
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await _context.Tags
                .Where(x => x.ArticleTagName == tagName)
                .SingleOrDefaultAsync();
        }

        public async Task<List<TagDto>> GetTagsForArticleAsync(int articleId)
        {
            return await _context.Tags
                .Where(x => x.Id == articleId)
                .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void UpdateTag(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
        }

        public async Task DeleteTag(int id)
        {
            var tag = await GetTagById(id);
            _context.Tags.Remove(tag);
        }
    }
}
