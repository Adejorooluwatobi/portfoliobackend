using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class ContactInquiry : BaseEntity
{
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string InquiryType { get; set; } = "general"; // "fulltime", "contract", "architecture", "general"
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public bool IsArchived { get; set; }
    public string? AdminNotes { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReadAt { get; set; }
}
