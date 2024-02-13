namespace KATCinema.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadPhotoAsync(string path);
    }
}
