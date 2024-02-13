using Imagekit;
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

        public async Task<string> UploadPhotoAsync(string path)
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

            byte[] bytes = File.ReadAllBytes(path);

            FileCreateRequest ob = new FileCreateRequest
            {
                file = bytes,
                fileName = Guid.NewGuid().ToString()
            };
            Result resp =  await _imagekit.UploadAsync(ob);

            string imageURL = _imagekit
                .Url(trans)
                .Src(resp.url)
                .TransformationPosition("query")
                .Generate();

            return imageURL;
        }
    }
}
