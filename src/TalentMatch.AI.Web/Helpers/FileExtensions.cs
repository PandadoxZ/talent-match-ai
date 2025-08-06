namespace TalentMatch.AI.Web.Helpers;

public static class FileExtensions
{
    public static string ConvertToMegabytes(this long bytes)
    {
        var megabytes = bytes / 1024.0 / 1024.0;
        
        return Math.Abs(megabytes - Math.Round(megabytes)) < 0.001 
            ? $"{Math.Round(megabytes)}"
            : $"{megabytes:F2}";
    }
}