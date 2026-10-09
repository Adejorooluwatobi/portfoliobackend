namespace Portfolio.Application.DTOs.Public;

public class ProfileResponseDto
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
    public string YearsExperienceSuffix { get; set; } = string.Empty;
    public string YearsExperienceLabel { get; set; } = string.Empty;
    public int ProjectsCompleted { get; set; }
    public string ProjectsCompletedSuffix { get; set; } = string.Empty;
    public string ProjectsLabel { get; set; } = string.Empty;

    public SiteSettingsDto SiteSettings { get; set; } = new();
    public HeroSectionDto HeroSection { get; set; } = new();
    public List<SocialLinkDto> SocialLinks { get; set; } = new();
    public List<PhilosophyCardDto> PhilosophyCards { get; set; } = new();
    public List<DisciplineCardDto> DisciplineCards { get; set; } = new();
}

public class SiteSettingsDto
{
    public string SiteTitle { get; set; } = string.Empty;
    public string Monogram { get; set; } = string.Empty;
    public string FaviconUrl { get; set; } = string.Empty;
    public string DefaultTheme { get; set; } = string.Empty;
    public string CopyrightText { get; set; } = string.Empty;
    public string FooterTagline { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
}

public class HeroSectionDto
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

public class SocialLinkDto
{
    public string Platform { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool ShowInHeader { get; set; }
    public bool ShowInFooter { get; set; }
    public bool ShowInSidebar { get; set; }
    public int SortOrder { get; set; }
}

public class PhilosophyCardDto
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class DisciplineCardDto
{
    public string IndexTag { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AccentColor { get; set; } = "#8b5cf6";
    public int SortOrder { get; set; }
    public List<string> Tags { get; set; } = new();
}
