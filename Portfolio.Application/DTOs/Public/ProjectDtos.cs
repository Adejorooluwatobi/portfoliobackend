namespace Portfolio.Application.DTOs.Public;

public class ProjectsResponseDto
{
    public List<ProjectCategoryDto> Categories { get; set; } = new();
    public List<ProjectSummaryDto> Projects { get; set; } = new();
}

public class ProjectCategoryDto
{
    public string Slug { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class ProjectSummaryDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string CategoryBadgeText { get; set; } = string.Empty;
    public string Timeframe { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? ImageAlt { get; set; }
    public string? IconKey { get; set; }
    public string? LiveUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? ArticleUrl { get; set; }
    public bool HasCaseStudy { get; set; }
    public int SortOrder { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<string> CategorySlugs { get; set; } = new();
}

public class ProjectDetailDto : ProjectSummaryDto
{
    public CaseStudyDto? CaseStudy { get; set; }
}

public class CaseStudyDto
{
    public string Title { get; set; } = string.Empty;
    public string CategoryLabel { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string? HeroImageUrl { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string? LiveUrl { get; set; }
    public List<string> Highlights { get; set; } = new();
    public List<string> Technologies { get; set; } = new();
}
