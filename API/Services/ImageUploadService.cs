using API.DTOs;
using API.Interfaces;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using API.Entities.Articles;

namespace API.Services
{
    public class ImageUploadService : IImageUploadService
    {
        public async Task<ArticleImageDto> AddImage(IFormFile file)
        {
            var uploadResult = new ArticleImageDto();

            if (file.Length > 0)
            {
                string fileName = file.FileName;
                string fileExtension = Path.GetExtension(fileName);
                // combining GUID to create unique name before saving in wwwroot
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                // getting full path inside wwwroot/images
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/articlephotos", uniqueFileName);

                // copying file
                file.CopyTo(new FileStream(imagePath, FileMode.Create));

                // FILE UPLOAD LOGIC END
                uploadResult.Url= uniqueFileName;
                //uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;

        }

        public Task DeleteImage(string imageName)
        {
            throw new NotImplementedException();
        }
    }
}
