using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class SkillCategory : BaseEntity
{
    public string Title { get; set; } = string.Empty;            // "Frontend Ecosystem"
    public string Subtitle { get; set; } = string.Empty;         // "Client interfaces & interaction"
    public string Icon { get; set; } = string.Empty;             // "web", "database", "cloud_sync"
    public string AccentColorToken { get; set; } = "primary";    // "primary", "secondary", "tertiary"
    public int SortOrder { get; set; }

    public List<SkillItem> Skills { get; set; } = new();
}

public class SkillItem : BaseEntity
{
    public Guid SkillCategoryId { get; set; }
    public SkillCategory SkillCategory { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public int? ProficiencyPercent { get; set; }                 // e.g. 95 (null for secondary tags)
    public bool IsPrimary { get; set; } = true;                  // true = has progress bar, false = pill tag
    public int SortOrder { get; set; }
}
