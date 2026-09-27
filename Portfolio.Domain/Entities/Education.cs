using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class Education : BaseEntity
{
    public string DegreeTitle { get; set; } = string.Empty;     // e.g. "B.Sc. Computer Science"
    public string InstitutionName { get; set; } = string.Empty; // e.g. "National Open University of Nigeria (NOUN)"
    public string Location { get; set; } = string.Empty;        // e.g. "Surulere, Lagos"
    public string DateRange { get; set; } = string.Empty;       // e.g. "2021 — 2024"
    public string Icon { get; set; } = "school";                // Material Symbol name
    public string Description { get; set; } = string.Empty;
    public string CredentialType { get; set; } = "Degree";      // "Degree", "Certification"
    public int SortOrder { get; set; }
}
