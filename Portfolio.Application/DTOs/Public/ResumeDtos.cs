namespace Portfolio.Application.DTOs.Public;

public class ResumeResponseDto
{
    public string Headline { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string CvFileUrl { get; set; } = string.Empty;
    public string CvDownloadName { get; set; } = string.Empty;
    public string PhilosophyStatement { get; set; } = string.Empty;

    public List<WorkExperienceDto> Experiences { get; set; } = new();
    public List<EducationDto> Educations { get; set; } = new();
    public List<SkillCategoryDto> SkillCategories { get; set; } = new();
}

public class WorkExperienceDto
{
    public Guid Id { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public bool IsCurrent { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AccentVariant { get; set; } = "primary";
    public int SortOrder { get; set; }
    public List<string> Technologies { get; set; } = new();
}

public class EducationDto
{
    public Guid Id { get; set; }
    public string DegreeTitle { get; set; } = string.Empty;
    public string InstitutionName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public string Icon { get; set; } = "school";
    public string Description { get; set; } = string.Empty;
    public string CredentialType { get; set; } = "Degree";
    public int SortOrder { get; set; }
}

public class SkillCategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string AccentColorToken { get; set; } = "primary";
    public int SortOrder { get; set; }
    public List<SkillItemDto> Skills { get; set; } = new();
}

public class SkillItemDto
{
    public string Name { get; set; } = string.Empty;
    public int? ProficiencyPercent { get; set; }
    public bool IsPrimary { get; set; }
    public int SortOrder { get; set; }
}
