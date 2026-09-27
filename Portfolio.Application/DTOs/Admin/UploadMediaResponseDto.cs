namespace Portfolio.Application.DTOs.Admin;

public class UploadMediaResponseDto
{
    public bool Success { get; set; }
    public string Url { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public long Bytes { get; set; }
    public string Message { get; set; } = string.Empty;
}
