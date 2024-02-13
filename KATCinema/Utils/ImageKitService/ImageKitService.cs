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

        public async Task<string> UploadPhotoAsync(IFormFile file)
        {
            Transformation trans = new Transformation()
                .Width(400)
                .Height(300)
                .AspectRatio("4-3")
                .Quality(100)
                .Crop("force")
                .CropMode("extract")
                .Focus("left")
                .Format("jpeg")
                .Raw("h-200,w-300,l-image,i-logo.png,l-end");

            var fileStream = file.OpenReadStream();
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            var bytes = memoryStream.ToArray();

            var fileCreateRequest = new FileCreateRequest
            {
                file = bytes,
                fileName = file.FileName
            };
            Result resp =  await _imagekit.UploadAsync(fileCreateRequest);

            string imageURL = _imagekit
                .Url(trans)
                .Src(resp.url)
                .Generate();

            Console.WriteLine(imageURL);

            return resp.url;
        }
    }
}
