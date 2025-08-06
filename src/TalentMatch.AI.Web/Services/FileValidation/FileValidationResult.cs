namespace TalentMatch.AI.Web.Services.FileValidation;

public class FileValidationResult
{
    public bool IsValid { get; private init; }
    public string? ErrorMessage { get; private init; }

    public static FileValidationResult Success() => new() { IsValid = true };
    public static FileValidationResult Error(string message) => new() { IsValid = false, ErrorMessage = message };
}