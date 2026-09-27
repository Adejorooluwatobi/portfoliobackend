using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class DisciplineCard : BaseEntity
{
    public string IndexTag { get; set; } = string.Empty; // e.g. "01 // FRONTEND"
    public string Icon { get; set; } = string.Empty;     // e.g. "palette"
    public string Title { get; set; } = string.Empty;    // e.g. "UI / UX Engineering"
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }

    public List<DisciplineCardTag> Tags { get; set; } = new();
}

public class DisciplineCardTag : BaseEntity
{
    public Guid DisciplineCardId { get; set; }
    public DisciplineCard DisciplineCard { get; set; } = null!;
    public string TagName { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
