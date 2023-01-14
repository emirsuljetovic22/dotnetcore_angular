using API.DTOs;
using API.Entities.Articles;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            var catToAdd = _mapper.Map<Category>(categoryDto);

            // Check if cat name already exist todo
            if (await _unitOfWork.CategoryRepository.CategoryNameExists(catToAdd.CategoryName, 0))
            {
                return BadRequest("Category with name " + catToAdd.CategoryName + "already exists.");
            }

            _unitOfWork.CategoryRepository.AddCategory(catToAdd);

            if (await _unitOfWork.Complete()) return Ok(_mapper.Map<Category>(catToAdd));

            return BadRequest("Failed to save the category.");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryToReturnDto>>(categories);

            return Ok(categoriesToReturn);
        }

        [HttpPost("add-to-article")]
        public async Task<ActionResult<Article>> AddCategoryToArticle(CategoryArticleDto categoryArticleDto)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleById(categoryArticleDto.ArticleId);
            var category = await _unitOfWork.CategoryRepository.GetCategoryById(categoryArticleDto.CategoryId);
            var prevCategory = await _unitOfWork.CategoryRepository.GetCategoryById(categoryArticleDto.CategoryId);

            if (prevCategory != null)
            {
                article.ArticleCategories.Remove(prevCategory);
                article.ArticleCategories.Add(category);

                if (await _unitOfWork.Complete())
                {
                    return Ok("Category " + prevCategory.Id + " is now replaced with " + category.Id);
                }
            }

            article.ArticleCategories.Add(category);

            if (await _unitOfWork.Complete())
            {
                return Ok("Category " + category.Id + " successfuly added to the article " + article.Title);
            }

            return BadRequest("Failed to add category to the article.");
        }

        [HttpDelete("remove-from-article")]
        public async Task<ActionResult<Article>> RemoveCategoryFromArticle(CategoryArticleDto categoryArticleDto)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleById(categoryArticleDto.ArticleId);
            var category = await _unitOfWork.CategoryRepository.GetCategoryById(categoryArticleDto.CategoryId);

            article.ArticleCategories.Remove(category);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to remove category from the article.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryToUpdateDto categoryToUpdateDto)
        {
            if (categoryToUpdateDto.CategoryName == null | categoryToUpdateDto.CategoryName == "")
            {
                return BadRequest("Category name field can not be empty.");
            }
            if (await _unitOfWork.CategoryRepository.CategoryNameExists(categoryToUpdateDto.CategoryName, id))
            {
                return BadRequest("Category " + categoryToUpdateDto.CategoryName + " already exists.");
            }

            if (id == categoryToUpdateDto.ParentCategoryId)
            {
                return BadRequest("Doesn't need explaining what went wrong here.");
            }

            var category = await _unitOfWork.CategoryRepository.GetCategoryById(id);

            _mapper.Map(categoryToUpdateDto, category);
            _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (await _unitOfWork.Complete()) return NoContent();
            return BadRequest("failed to update the category");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            await _unitOfWork.CategoryRepository.DeleteCategory(id);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete category from the database.");
        }
    }
}
