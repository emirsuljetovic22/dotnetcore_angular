using API.DTOs;
using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IImageUploadService
    {
        Task<ArticleImageDto> AddImage(IFormFile file);
        Task DeleteImage(string imageName);
    }
}
