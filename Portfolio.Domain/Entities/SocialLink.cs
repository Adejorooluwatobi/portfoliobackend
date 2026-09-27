using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class SocialLink : BaseEntity
{
    public string Platform { get; set; } = string.Empty; // "github", "linkedin", "twitter", "facebook", "instagram"
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool ShowInHeader { get; set; }
    public bool ShowInFooter { get; set; }
    public bool ShowInSidebar { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
