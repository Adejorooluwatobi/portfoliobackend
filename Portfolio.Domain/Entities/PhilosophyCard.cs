using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class PhilosophyCard : BaseEntity
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AccentColor { get; set; } = "#8b5cf6";
    public int SortOrder { get; set; }
}
