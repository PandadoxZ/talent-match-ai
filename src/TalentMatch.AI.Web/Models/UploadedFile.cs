using Microsoft.AspNetCore.Components.Forms;

namespace TalentMatch.AI.Web.Models;

public class UploadedFile
{
    public string FileName { get; set; } = string.Empty;
    public long Size { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public IBrowserFile File { get; set; } = null!;
    public byte[]? Content { get; set; }
    public string? ExtractedText { get; set; }
}