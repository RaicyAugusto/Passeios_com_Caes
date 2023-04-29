using CloudinaryDotNet.Actions;

namespace PasseiosComCaes.Interfaces;

public interface IFotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}