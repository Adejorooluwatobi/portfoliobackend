using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class WorkExperience : BaseEntity
{
    public string JobTitle { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty; // "Full-Time", "Contract"
    public string DateRange { get; set; } = string.Empty;       // "2024 — Present"
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AccentVariant { get; set; } = "primary";      // "primary", "secondary", "neutral"
    public int SortOrder { get; set; }

    public List<ExperienceTechnology> Technologies { get; set; } = new();
}

public class ExperienceTechnology : BaseEntity
{
    public Guid WorkExperienceId { get; set; }
    public WorkExperience WorkExperience { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
