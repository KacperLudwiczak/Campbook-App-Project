namespace Campbook_App.Services;

public interface ICloudinaryService
{
    Task<Image> UploadAsync(IFormFile file);
    Task DeleteAsync(string filename);
}
