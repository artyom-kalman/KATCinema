namespace KATCinema.Interfaces
{
    public interface IPhotoService
    {
        Task<Result> UploadPhotoAsync(IFormFile file);
        Task<bool> DeletePhotoAsync(string photo);
    }
}
