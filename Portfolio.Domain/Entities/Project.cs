using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class ProjectCategory : BaseEntity
{
    public string Slug { get; set; } = string.Empty;  // "all", "fullstack", "frontend", "ai-backend"
    public string Label { get; set; } = string.Empty; // "All", "Fullstack", "Frontend", "AI & Backend"
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;

    public List<ProjectCategoryMap> ProjectMaps { get; set; } = new();
}

public class Project : BaseEntity
{
    public string Slug { get; set; } = string.Empty;            // "tslhub", "comza", "aitoolkit", "gateway"
    public string Title { get; set; } = string.Empty;           // "TSL Hub Law Firm Platform"
    public string ClientName { get; set; } = string.Empty;      // "TSL Hub"
    public string CategoryBadgeText { get; set; } = string.Empty; // "Legal Tech • Fullstack"
    public string Timeframe { get; set; } = string.Empty;       // "2023 — 2024"
    public string ShortDescription { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }                       // "assets/img/work/logo (1).png"
    public string? ImageAlt { get; set; }
    public string? IconKey { get; set; }                        // "dns" for vector/icon cards
    public string? LiveUrl { get; set; }                        // "https://tslhub.org/index.html"
    public string? GithubUrl { get; set; }                      // "https://github.com"
    public string? ArticleUrl { get; set; }                     // "articles.html"
    public bool HasCaseStudy { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }

    public List<ProjectTag> Tags { get; set; } = new();
    public List<ProjectCategoryMap> CategoryMaps { get; set; } = new();
    public CaseStudy? CaseStudy { get; set; }
}

public class ProjectTag : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    public string TagName { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class ProjectCategoryMap
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public Guid ProjectCategoryId { get; set; }
    public ProjectCategory ProjectCategory { get; set; } = null!;
}
