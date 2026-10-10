namespace Portfolio.Application.DTOs.Admin;

public class UpdateProfileRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public string PrimaryTitle { get; set; } = string.Empty;
    public string SidebarTitle { get; set; } = string.Empty;
    public string AvatarImageUrl { get; set; } = string.Empty;
    public string AvatarAltText { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhoneDisplay { get; set; } = string.Empty;
    public string WhatsappUrl { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LocationDisplay { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public string CvFileUrl { get; set; } = string.Empty;
    public string CvDownloadName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string AvailabilityText { get; set; } = string.Empty;
    public string EmploymentStatus { get; set; } = string.Empty;
    public string ResponseTime { get; set; } = string.Empty;
    public string EngagementScope { get; set; } = string.Empty;
    public string ResponseGuarantee { get; set; } = string.Empty;
    public int YearsExperience { get; set; }
    public string YearsExperienceSuffix { get; set; } = "4+";
    public string YearsExperienceLabel { get; set; } = "Production Systems";
    public int ProjectsCompleted { get; set; }
    public string ProjectsCompletedSuffix { get; set; } = "25+";
    public string ProjectsLabel { get; set; } = "Web & Cloud APIs";
}

public class SocialLinkCreateUpdateDto
{
    public string Platform { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool ShowInHeader { get; set; }
    public bool ShowInFooter { get; set; }
    public bool ShowInSidebar { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class HeroSectionUpdateDto
{
    public string CategoryBadgeText { get; set; } = string.Empty;
    public string CategoryBadgeIcon { get; set; } = string.Empty;
    public string Headline { get; set; } = string.Empty;
    public string BioLead { get; set; } = string.Empty;
    public string BioFrontend { get; set; } = string.Empty;
    public string BioBackend { get; set; } = string.Empty;
    public string PrimaryCtaText { get; set; } = string.Empty;
    public string PrimaryCtaUrl { get; set; } = string.Empty;
    public string PrimaryCtaIcon { get; set; } = string.Empty;
    public string SecondaryCtaText { get; set; } = string.Empty;
    public string SecondaryCtaUrl { get; set; } = string.Empty;
    public string SecondaryCtaIcon { get; set; } = string.Empty;
    public string TertiaryCtaText { get; set; } = string.Empty;
    public string TertiaryCtaUrl { get; set; } = string.Empty;
    public string TertiaryCtaIcon { get; set; } = string.Empty;
}

public class PhilosophyCardCreateUpdateDto
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? AccentColor { get; set; }
    public int SortOrder { get; set; }
}

public class DisciplineCardCreateUpdateDto
{
    public string IndexTag { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? AccentColor { get; set; }
    public int SortOrder { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class WorkExperienceCreateUpdateDto
{
    public string JobTitle { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AccentVariant { get; set; } = "primary";
    public int SortOrder { get; set; }
    public List<string> Technologies { get; set; } = new();
}

public class EducationCreateUpdateDto
{
    public string DegreeTitle { get; set; } = string.Empty;
    public string InstitutionName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string DateRange { get; set; } = string.Empty;
    public string Icon { get; set; } = "school";
    public string Description { get; set; } = string.Empty;
    public string CredentialType { get; set; } = "Degree";
    public int SortOrder { get; set; }
}

public class SkillCategoryCreateUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string AccentColorToken { get; set; } = "primary";
    public int SortOrder { get; set; }
}

public class SkillItemCreateUpdateDto
{
    public Guid SkillCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ProficiencyPercent { get; set; }
    public bool IsPrimary { get; set; } = true;
    public int SortOrder { get; set; }
}

public class ProjectCreateUpdateDto
{
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
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<Guid> CategoryIds { get; set; } = new();
}

public class CaseStudyUpdateDto
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

public class ProjectCategoryCreateUpdateDto
{
    public string Slug { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ArticleCreateUpdateDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string PublicationType { get; set; } = "Published";
    public string PublishStatus { get; set; } = "Published";
    public int ReadTimeMinutes { get; set; } = 5;
    public string? ImageUrl { get; set; }
    public string? ImageAlt { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? FooterAnnotation { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<ArticleLinkCreateDto> Links { get; set; } = new();
}

public class ArticleLinkCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
}

public class SiteSettingsUpdateDto
{
    public string SiteTitle { get; set; } = string.Empty;
    public string Monogram { get; set; } = string.Empty;
    public string FaviconUrl { get; set; } = string.Empty;
    public string DefaultTheme { get; set; } = "dark";
    public string CopyrightText { get; set; } = string.Empty;
    public string FooterTagline { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string? FormspreeEndpoint { get; set; }
}

public class PageSettingsUpdateDto
{
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string? HeroBadgeIcon { get; set; }
    public string? HeroBadgeText { get; set; }
    public string? HeroHeading { get; set; }
    public string? HeroSubtitle { get; set; }
    public string? CalloutHeadline { get; set; }
    public string? CalloutBody { get; set; }
    public string? CalloutButtonText { get; set; }
    public string? CalloutButtonHref { get; set; }
}

public class NavItemCreateUpdateDto
{
    public string Label { get; set; } = string.Empty;
    public string MobileLabel { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string DataPage { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class InquiryItemDto
{
    public Guid Id { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string InquiryType { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public bool IsArchived { get; set; }
    public string? AdminNotes { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}

public class InquiryStatsDto
{
    public int TotalCount { get; set; }
    public int UnreadCount { get; set; }
    public int ArchivedCount { get; set; }
    public int TodayCount { get; set; }
}

public class UpdateInquiryNotesDto
{
    public string Notes { get; set; } = string.Empty;
}
