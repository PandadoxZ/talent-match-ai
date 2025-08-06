using TalentMatch.AI.Web.Models;

namespace TalentMatch.AI.Web.Services.FileValidation;

public interface IFileValidationService
{
    Task<FileValidationResult> ValidateFileAsync(UploadedFile file, IEnumerable<FileType> allowedFormats,
        long maxSizeInBytes);
}