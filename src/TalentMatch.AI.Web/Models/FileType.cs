namespace TalentMatch.AI.Web.Models;

public record FileType(string Name, string[] Extensions, string[] ContentTypes, byte[] Signatures)
{
    public static readonly FileType PlainText = new("TXT", [".txt"], ["text/plain"], []);
    public static readonly FileType Pdf = new("PDF", [".pdf"], ["application/pdf"], "%PDF"u8.ToArray());

    public static readonly FileType MsWord = new("DOCX", [".docx"],
        ["application/vnd.openxmlformats-officedocument.wordprocessingml.document"], [0x50, 0x4B, 0x03, 0x04]);
}