using API.DTOs;
using API.Entities;
using API.Entities.Articles;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TagController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<ActionResult<TagDto>> CreateTag(TagDto tagDto)
        {
            var tagToAdd = _mapper.Map<Tag>(tagDto);

            _unitOfWork.TagRepository.AddTag(tagToAdd);

            if (await _unitOfWork.Complete()) return Ok(_mapper.Map<Tag>(tagDto));

            return BadRequest("Failed to save the tag.");
        }

        [HttpPost("add-to-article")]
        public async Task<ActionResult<Article>> AddTagToArticle(TagArticleDto addTagToArticleDto)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleById(addTagToArticleDto.ArticleId);
            var tag = await _unitOfWork.TagRepository.GetTagById(addTagToArticleDto.TagId);
            
            article.Tags.Add(tag);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to add tag to the article.");
        }

        [HttpDelete("remove-from-article")]
        public async Task<ActionResult<Article>> RemoveTagFromArticle(TagArticleDto addTagToArticleDto)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleById(addTagToArticleDto.ArticleId);
            var tag = await _unitOfWork.TagRepository.GetTagById(addTagToArticleDto.TagId);

            article.Tags.Remove(tag);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to remove tag from the article.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _unitOfWork.TagRepository.GetAllTags();

            var tagsToReturn = _mapper.Map<IEnumerable<TagDto>>(tags);

            return Ok(tagsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTagById(int id)
        {
            var tagToReturn = await _unitOfWork.TagRepository.GetTagById(id);

            return Ok(tagToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTag(int id, TagDto tagDto)
        {

            var tag = await _unitOfWork.TagRepository.GetTagById(id);

            _mapper.Map(tagDto, tag);
            _unitOfWork.TagRepository.UpdateTag(tag);

            if (await _unitOfWork.Complete()) return NoContent();
            return BadRequest("failed to update the tag");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> DeleteTag(int id)
        {
            await _unitOfWork.TagRepository.DeleteTag(id);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to delete tag from the database.");
        }
    }
}
