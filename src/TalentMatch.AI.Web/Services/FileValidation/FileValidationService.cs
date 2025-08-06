using TalentMatch.AI.Web.Helpers;
using TalentMatch.AI.Web.Models;

namespace TalentMatch.AI.Web.Services.FileValidation;

public class FileValidationService : IFileValidationService
{
    public async Task<FileValidationResult> ValidateFileAsync(UploadedFile file, IEnumerable<FileType> allowedFiles, long maxSizeInBytes)
    {
        try
        {
            var allowedFilesList = allowedFiles.ToList();

            // Size validation
            if (file.Size > maxSizeInBytes)
            {
                return FileValidationResult.Error($"File size exceeds the maximum allowed size of {maxSizeInBytes.ConvertToMegabytes()} MB");
            }

            // Extension validation
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var normalizedAllowedExtensions = allowedFilesList.SelectMany(ft => ft.Extensions)
                .Select(e => e.ToLowerInvariant().Trim())
                .Select(e => e.StartsWith('.') ? e : $".{e}")
                .ToHashSet();

            if (!normalizedAllowedExtensions.Contains(fileExtension))
            {
                return FileValidationResult.Error($"File type {fileExtension} is not allowed. Allowed types: {string.Join(", ", normalizedAllowedExtensions)}");
            }
            
            var matchingFileType = allowedFilesList.FirstOrDefault(ft => 
                ft.Extensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase));

            if (matchingFileType != null)
            {
                // Content Type validation
                if (!matchingFileType.ContentTypes.Contains(file.ContentType.ToLowerInvariant()))
                {
                    return FileValidationResult.Error($"Invalid file content type: {file.ContentType}");
                }
                
                // File Signature validation
                if (matchingFileType.Signatures.Length > 0)
                {
                    var isValidSignature = await ValidateFileSignatureAsync(file, matchingFileType.Signatures);
                    if (!isValidSignature)
                    {
                        return FileValidationResult.Error("File content does not match its extension");
                    }
                }
            }
            
            return FileValidationResult.Success();
        }
        catch (Exception)
        {
            return FileValidationResult.Error("File validation failed.");
        }
    }
    
    private static async Task<bool> ValidateFileSignatureAsync(UploadedFile file, byte[] expectedSignature)
    {
        await using var stream = file.File.OpenReadStream(maxAllowedSize: file.Size);
        var buffer = new byte[expectedSignature.Length];
        var bytesRead = await stream.ReadAsync(buffer);

        return bytesRead >= expectedSignature.Length && buffer.SequenceEqual(expectedSignature);
    }
}