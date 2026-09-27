using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class Article : BaseEntity
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string PublicationType { get; set; } = "Published"; // "Software Development Guide", "LinkedIn Technical Post"
    public string PublishStatus { get; set; } = "Published";
    public int ReadTimeMinutes { get; set; } = 5;
    public string? ImageUrl { get; set; }
    public string? ImageAlt { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? FooterAnnotation { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }

    public List<ArticleTag> Tags { get; set; } = new();
}

public class ArticleTag : BaseEntity
{
    public Guid ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public string TagName { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
