using API.DTOs;
using API.Entities.Articles;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }
        public async Task<List<CategoryToReturnDto>> GetAll()
        {
            return await _context.Categories
                .Where(pc => pc.ParentCategoryId == null)
                // Define how deep category tree can go, find better way - TDO
                .Include(sc => sc.SubCategories.Where(sc => sc.ParentCategoryId != null)) // level 1
                .ThenInclude(sc => sc.SubCategories.Where(sc => sc.ParentCategoryId != null)) // level 2
                .ThenInclude(sc => sc.SubCategories.Where(sc => sc.ParentCategoryId != null)) // level 3
                .ProjectTo<CategoryToReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            _context.Categories.Remove(category);
        }

        public async Task<bool> CategoryNameExists(string categoryName, int categoryId)
        {
            if (await _context.Categories.AnyAsync(x => x.CategoryName == categoryName && x.Id != categoryId))
                return true;

            return false;
        }

    }
}
