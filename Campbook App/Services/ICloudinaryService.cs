namespace Campbook_App.Services;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task DeleteImageAsync(string publicId);
}

