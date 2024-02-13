using Humanizer.Bytes;
using Imagekit;
using Imagekit.Models;
using Imagekit.Sdk;
using KATCinema.Interfaces;
using KATCinema.Utils.ImageKitService;
using Microsoft.Extensions.Options;

namespace KATCinema.Utils.ImageKitHelper
{
    public class ImageKitService : IPhotoService
    {
        private ImagekitClient _imagekit;

        public ImageKitService(IOptions<ImageKitSettings> options)
        {
            _imagekit = new ImagekitClient(
                options.Value.PublicKey,
                options.Value.PrivateKey,
                options.Value.UrlEndpoint
            );
        }

        public async Task<Result> UploadPhotoAsync(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();

            var fileCreateRequest = new FileCreateRequest
            {
                file = bytes,
                fileName = file.FileName
            };
            Result uploadResponse =  await _imagekit.UploadAsync(fileCreateRequest);

            return uploadResponse;
        }   

        public async Task<bool> DeletePhotoAsync(string photoId)
        {
            ResultDelete result = await _imagekit.DeleteFileAsync(photoId);
            if (result.HttpStatusCode != 204)
            {
                return false;
            }
            return true;
        }
    }
}
