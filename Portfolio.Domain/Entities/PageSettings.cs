using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class PageSettings : BaseEntity
{
    public string PageKey { get; set; } = string.Empty; // "index", "resume", "portfolio", "articles", "contact"
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string? HeroBadgeIcon { get; set; }
    public string? HeroBadgeText { get; set; }
    public string? HeroHeading { get; set; }
    public string? HeroSubtitle { get; set; }
    public string? CalloutHeadline { get; set; }
    public string? CalloutBody { get; set; }
    public string? CalloutButtonText { get; set; }
    public string? CalloutButtonHref { get; set; }
}

public class NavItem : BaseEntity
{
    public string Label { get; set; } = string.Empty;       // "About", "Resume & Skills", etc.
    public string MobileLabel { get; set; } = string.Empty; // "About Overview", etc.
    public string Url { get; set; } = string.Empty;         // "index.html"
    public string Icon { get; set; } = string.Empty;        // Material symbol name: "person", "description"
    public string DataPage { get; set; } = string.Empty;    // "index.html"
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
