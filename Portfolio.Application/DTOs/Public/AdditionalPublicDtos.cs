namespace Portfolio.Application.DTOs.Public;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string PublicationType { get; set; } = "Published";
    public string PublishStatus { get; set; } = "Published";
    public int ReadTimeMinutes { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageAlt { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? FooterAnnotation { get; set; }
    public DateTime? PublishedAt { get; set; }
    public int SortOrder { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class ContactInfoDto
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PhoneDisplay { get; set; } = string.Empty;
    public string WhatsappUrl { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string LocationDisplay { get; set; } = string.Empty;
    public string ResponseTime { get; set; } = string.Empty;
    public string ResponseGuarantee { get; set; } = string.Empty;
    public string EngagementScope { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string AvailabilityText { get; set; } = string.Empty;
}

public class CreateInquiryRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string InquiryType { get; set; } = "general";
    public string Message { get; set; } = string.Empty;
}

public class CreateInquiryResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid? InquiryId { get; set; }
}

public class NavigationResponseDto
{
    public string Monogram { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PrimaryTitle { get; set; } = string.Empty;
    public string CvFileUrl { get; set; } = string.Empty;
    public string CvDownloadName { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string AvailabilityText { get; set; } = string.Empty;
    public List<NavItemDto> NavItems { get; set; } = new();
    public List<SocialLinkDto> HeaderSocials { get; set; } = new();
    public List<SocialLinkDto> FooterSocials { get; set; } = new();
    public string CopyrightText { get; set; } = string.Empty;
    public string FooterTagline { get; set; } = string.Empty;
}

public class NavItemDto
{
    public string Label { get; set; } = string.Empty;
    public string MobileLabel { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string DataPage { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}

public class PageSettingsDto
{
    public string PageKey { get; set; } = string.Empty;
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
