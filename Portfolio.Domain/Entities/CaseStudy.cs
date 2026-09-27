using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class CaseStudy : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string CategoryLabel { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string? HeroImageUrl { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string? LiveUrl { get; set; }

    public List<CaseStudyHighlight> Highlights { get; set; } = new();
    public List<CaseStudyTechnology> Technologies { get; set; } = new();
}

public class CaseStudyHighlight : BaseEntity
{
    public Guid CaseStudyId { get; set; }
    public CaseStudy CaseStudy { get; set; } = null!;
    public string HighlightText { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class CaseStudyTechnology : BaseEntity
{
    public Guid CaseStudyId { get; set; }
    public CaseStudy CaseStudy { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
