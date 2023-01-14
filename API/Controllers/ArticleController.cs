using API.DTOs;
using API.Entities;
using API.Entities.Articles;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageUploadService _imageUploadService;
        public ArticleController(IMapper mapper, IUnitOfWork unitOfWork, IImageUploadService imageUploadService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageUploadService = imageUploadService;
        }

        [HttpPost("upload-image/{articleId}")]
        public async Task<IActionResult> UploadImage([FromForm] ArticleImage articleImage, int articleId)
        {

            var article = await _unitOfWork.ArticleRepository.GetArticleById(articleId);

            var imageUrl = await _imageUploadService.AddImage(articleImage.Image);

            var createdImage = new ArticleImage { 
                Url = imageUrl.Url
            };

            if (article.ArticleImages.Count == 0)
            {
                createdImage.Highlighted = true;
            }

            article.ArticleImages.Add(createdImage);

            if (await _unitOfWork.Complete()) return Ok(imageUrl.Url);

            return Ok();
        }

        [HttpPut("set-main-image/{articleId}/{imageId}")]

        public async Task<ActionResult> SetMainImage (int articleId, int imageId)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleById(articleId);

            var image = article.ArticleImages.FirstOrDefault(x => x.Id == imageId);

            if (image.Highlighted) return BadRequest("This is already main article image");

            var currentMain = article.ArticleImages.FirstOrDefault(x => x.Highlighted);

            if (currentMain != null) currentMain.Highlighted = false;

            image.Highlighted = true;

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("failed to set main article image");
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle([FromForm] ArticleAddUpdateDto articleAddUpdateDto)
        {
            if (await _unitOfWork.ArticleRepository.UrlExists(articleAddUpdateDto.Url)) return BadRequest("Url already exists.");

            var files = HttpContext.Request.Form.Files;
            var tags = HttpContext.Request.Form["tags"];

            var articleToAdd = new Article
            {
                Title = articleAddUpdateDto.Title,
                Content = articleAddUpdateDto.Content,
                ShortDescription = articleAddUpdateDto.ShortDescription,
                Url = articleAddUpdateDto.Url,
                Created = new DateTime(),
                Updated = new DateTime(),
                Author = articleAddUpdateDto.Author
            };

            _unitOfWork.ArticleRepository.AddArticle(articleToAdd);

            if (await _unitOfWork.Complete()) {

                var createdArticle = await _unitOfWork.ArticleRepository.GetArticleById(articleToAdd.Id);

                if (files != null && files.Count > 0)
                {
                    foreach (var imageReceived in files)
                    {
                        var imageUrl = await _imageUploadService.AddImage(imageReceived);
                        var createdImage = new ArticleImage
                        {
                            Url = imageUrl.Url
                        };
                        createdArticle.ArticleImages.Add(createdImage);
                        await _unitOfWork.Complete();
                    }
                }

                if (articleAddUpdateDto.Category != 0)
                {
                    var category = await _unitOfWork.CategoryRepository.GetCategoryById(articleAddUpdateDto.Category);
                    
                    createdArticle.ArticleCategories.Add(category);
                    await _unitOfWork.Complete();
                }

                if (tags.Count > 0)
                {
                    foreach (var tag in articleAddUpdateDto.Tags)
                    {
                        var tagReceived = await _unitOfWork.TagRepository.GetTagById(tag.Id);
                        createdArticle.Tags.Add(tagReceived);
                        await _unitOfWork.Complete();
                    }
                }
                return Ok(articleToAdd);
            }

            return BadRequest("Failed to add the article.");
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery] PaginationParams paginationParams)
        {
            var articles = await _unitOfWork.ArticleRepository.GetArticlesAsync(paginationParams);

            var articlesToReturn = _mapper.Map<IEnumerable<ArticleListDto>>(articles);

            Response.AddPaginationHeader(articles.CurrentPage, articles.PageSize, articles.TotalCount, articles.TotalPages);

            return Ok(articlesToReturn);
        }

        [HttpGet("{articleUrl}")]
        public async Task<ActionResult<Article>> GetArticleByUrl(string articleUrl)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleByUrl(articleUrl);

            var articleToReturn = _mapper.Map<ArticleDto>(article);

            return Ok(articleToReturn);
        }

        [HttpPut("{url}")]
        public async Task<ActionResult> UpdateArticle(string url, ArticleAddUpdateDto articleUpdateDto)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleByUrl(url);
            var existingArticleTags = article.Tags;

            if (article.Url != articleUpdateDto.Url)
            {
                if (await _unitOfWork.ArticleRepository.UrlExists(articleUpdateDto.Url))
                {
                    return BadRequest("Url already exists.");
                }
            }

            if (articleUpdateDto.Category != 0)
            {
                var category = await _unitOfWork.CategoryRepository.GetCategoryById(articleUpdateDto.Category);
                if (article.ArticleCategories != null)
                {
                    var prevCategory = article.ArticleCategories.FirstOrDefault();
                    article.ArticleCategories.Remove(prevCategory);
                }
                article.ArticleCategories.Add(category);
            }

            if (articleUpdateDto.Tags != null)
            {
                foreach(var existingTag in existingArticleTags)
                {
                    article.Tags.Remove(existingTag);
                }
                
                foreach(var tag in articleUpdateDto.Tags)
                {
                    var tagReceived = await _unitOfWork.TagRepository.GetTagById(tag.Id);
                    article.Tags.Add(tagReceived);
                }
            }

            var articleToUpdate = new ArticleOnlyDto
            {
                Title = articleUpdateDto.Title,
                Content = articleUpdateDto.Content,
                ShortDescription = articleUpdateDto.ShortDescription,
                Url = articleUpdateDto.Url,
                Updated = new DateTime(),
                Author = articleUpdateDto.Author
            };

            _mapper.Map(articleToUpdate, article);
            _unitOfWork.ArticleRepository.UpdateArticle(article);

            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("failed to update the article");
        }

        [HttpDelete("{url}")]
        public async Task<ActionResult<Article>> DeleteArticle(string url)
        {
            await _unitOfWork.ArticleRepository.DeleteArticle(url);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete article from the database.");
        }
    }
}
