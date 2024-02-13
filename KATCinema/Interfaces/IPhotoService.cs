namespace KATCinema.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadPhotoAsync(IFormFile file);
    }
}
