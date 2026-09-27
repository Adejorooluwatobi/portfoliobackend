using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class PhilosophyCard : BaseEntity
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}
