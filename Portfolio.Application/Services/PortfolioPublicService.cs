using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.DTOs.Public;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public interface IPortfolioPublicService
{
    Task<ProfileResponseDto?> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<ResumeResponseDto?> GetResumeAsync(CancellationToken cancellationToken = default);
    Task<ProjectsResponseDto> GetProjectsAsync(string? categorySlug = null, CancellationToken cancellationToken = default);
    Task<ProjectDetailDto?> GetProjectBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<ArticleDto>> GetArticlesAsync(CancellationToken cancellationToken = default);
    Task<ContactInfoDto?> GetContactInfoAsync(CancellationToken cancellationToken = default);
    Task<NavigationResponseDto> GetNavigationAsync(CancellationToken cancellationToken = default);
    Task<PageSettingsDto?> GetPageSettingsAsync(string pageKey, CancellationToken cancellationToken = default);
    Task<CreateInquiryResponseDto> SubmitContactInquiryAsync(CreateInquiryRequestDto request, CancellationToken cancellationToken = default);
}

public class PortfolioPublicService : IPortfolioPublicService
{
    private readonly IAppDbContext _context;
    private readonly IEmailService _emailService;

    public PortfolioPublicService(IAppDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<ProfileResponseDto?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (profile == null) return null;

        var siteSettings = await _context.SiteSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var hero = await _context.HeroSections.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var socials = await _context.SocialLinks.AsNoTracking().Where(s => s.IsActive).OrderBy(s => s.SortOrder).ToListAsync(cancellationToken);
        var philosophies = await _context.PhilosophyCards.AsNoTracking().OrderBy(p => p.SortOrder).ToListAsync(cancellationToken);
        var disciplines = await _context.DisciplineCards.AsNoTracking().Include(d => d.Tags).OrderBy(d => d.SortOrder).ToListAsync(cancellationToken);

        return new ProfileResponseDto
        {
            FullName = profile.FullName,
            PrimaryTitle = profile.PrimaryTitle,
            SidebarTitle = profile.SidebarTitle,
            AvatarImageUrl = profile.AvatarImageUrl,
            AvatarAltText = profile.AvatarAltText,
            Email = profile.Email,
            Phone = profile.Phone,
            PhoneDisplay = profile.PhoneDisplay,
            WhatsappUrl = profile.WhatsappUrl,
            Location = profile.Location,
            LocationDisplay = profile.LocationDisplay,
            Birthday = profile.Birthday,
            CvFileUrl = profile.CvFileUrl,
            CvDownloadName = profile.CvDownloadName,
            IsAvailable = profile.IsAvailable,
            AvailabilityText = profile.AvailabilityText,
            EmploymentStatus = profile.EmploymentStatus,
            ResponseTime = profile.ResponseTime,
            EngagementScope = profile.EngagementScope,
            ResponseGuarantee = profile.ResponseGuarantee,
            YearsExperience = profile.YearsExperience,
            YearsExperienceSuffix = profile.YearsExperienceSuffix,
            YearsExperienceLabel = profile.YearsExperienceLabel,
            ProjectsCompleted = profile.ProjectsCompleted,
            ProjectsCompletedSuffix = profile.ProjectsCompletedSuffix,
            ProjectsLabel = profile.ProjectsLabel,

            SiteSettings = siteSettings != null ? new SiteSettingsDto
            {
                SiteTitle = siteSettings.SiteTitle,
                Monogram = siteSettings.Monogram,
                FaviconUrl = siteSettings.FaviconUrl,
                DefaultTheme = siteSettings.DefaultTheme,
                CopyrightText = siteSettings.CopyrightText,
                FooterTagline = siteSettings.FooterTagline,
                MetaDescription = siteSettings.MetaDescription
            } : new SiteSettingsDto(),

            HeroSection = hero != null ? new HeroSectionDto
            {
                CategoryBadgeText = hero.CategoryBadgeText,
                CategoryBadgeIcon = hero.CategoryBadgeIcon,
                Headline = hero.Headline,
                BioLead = hero.BioLead,
                BioFrontend = hero.BioFrontend,
                BioBackend = hero.BioBackend,
                PrimaryCtaText = hero.PrimaryCtaText,
                PrimaryCtaUrl = hero.PrimaryCtaUrl,
                PrimaryCtaIcon = hero.PrimaryCtaIcon,
                SecondaryCtaText = hero.SecondaryCtaText,
                SecondaryCtaUrl = hero.SecondaryCtaUrl,
                SecondaryCtaIcon = hero.SecondaryCtaIcon,
                TertiaryCtaText = hero.TertiaryCtaText,
                TertiaryCtaUrl = hero.TertiaryCtaUrl,
                TertiaryCtaIcon = hero.TertiaryCtaIcon
            } : new HeroSectionDto(),

            SocialLinks = socials.Select(s => new SocialLinkDto
            {
                Platform = s.Platform,
                Title = s.Title,
                Url = s.Url,
                Icon = s.Icon,
                ShowInHeader = s.ShowInHeader,
                ShowInFooter = s.ShowInFooter,
                ShowInSidebar = s.ShowInSidebar,
                SortOrder = s.SortOrder
            }).ToList(),

            PhilosophyCards = philosophies.Select(p => new PhilosophyCardDto
            {
                Icon = p.Icon,
                Title = p.Title,
                Description = p.Description,
                SortOrder = p.SortOrder
            }).ToList(),

            DisciplineCards = disciplines.Select(d => new DisciplineCardDto
            {
                IndexTag = d.IndexTag,
                Icon = d.Icon,
                Title = d.Title,
                Description = d.Description,
                SortOrder = d.SortOrder,
                Tags = d.Tags.OrderBy(t => t.SortOrder).Select(t => t.TagName).ToList()
            }).ToList()
        };
    }

    public async Task<ResumeResponseDto?> GetResumeAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var pageSetting = await _context.PageSettings.AsNoTracking().FirstOrDefaultAsync(p => p.PageKey == "resume", cancellationToken);
        var experiences = await _context.WorkExperiences.AsNoTracking().Include(w => w.Technologies).OrderBy(w => w.SortOrder).ToListAsync(cancellationToken);
        var educations = await _context.Educations.AsNoTracking().OrderBy(e => e.SortOrder).ToListAsync(cancellationToken);
        var skillCategories = await _context.SkillCategories.AsNoTracking().Include(s => s.Skills).OrderBy(s => s.SortOrder).ToListAsync(cancellationToken);

        return new ResumeResponseDto
        {
            Headline = pageSetting?.HeroHeading ?? "Experience, Credentials & Skills",
            Summary = pageSetting?.HeroSubtitle ?? "4+ continuous years engineering production web applications, distributed APIs, microservices, and responsive user interfaces.",
            CvFileUrl = profile?.CvFileUrl ?? "assets/doc/OLUWATOBI_Adejoro_CV(Fullstack).docx",
            CvDownloadName = profile?.CvDownloadName ?? "OLUWATOBI_Adejoro_CV.docx",
            PhilosophyStatement = "I believe clean architecture, automated testing, and thoughtful system design produce software that scales reliably and reduces maintenance debt over years of production use.",

            Experiences = experiences.Select(e => new WorkExperienceDto
            {
                Id = e.Id,
                JobTitle = e.JobTitle,
                CompanyName = e.CompanyName,
                EmploymentType = e.EmploymentType,
                DateRange = e.DateRange,
                IsCurrent = e.IsCurrent,
                Description = e.Description,
                AccentVariant = e.AccentVariant,
                SortOrder = e.SortOrder,
                Technologies = e.Technologies.OrderBy(t => t.SortOrder).Select(t => t.Name).ToList()
            }).ToList(),

            Educations = educations.Select(ed => new EducationDto
            {
                Id = ed.Id,
                DegreeTitle = ed.DegreeTitle,
                InstitutionName = ed.InstitutionName,
                Location = ed.Location,
                DateRange = ed.DateRange,
                Icon = ed.Icon,
                Description = ed.Description,
                CredentialType = ed.CredentialType,
                SortOrder = ed.SortOrder
            }).ToList(),

            SkillCategories = skillCategories.Select(sc => new SkillCategoryDto
            {
                Id = sc.Id,
                Title = sc.Title,
                Subtitle = sc.Subtitle,
                Icon = sc.Icon,
                AccentColorToken = sc.AccentColorToken,
                SortOrder = sc.SortOrder,
                Skills = sc.Skills.OrderBy(s => s.SortOrder).Select(s => new SkillItemDto
                {
                    Name = s.Name,
                    ProficiencyPercent = s.ProficiencyPercent,
                    IsPrimary = s.IsPrimary,
                    SortOrder = s.SortOrder
                }).ToList()
            }).ToList()
        };
    }

    public async Task<ProjectsResponseDto> GetProjectsAsync(string? categorySlug = null, CancellationToken cancellationToken = default)
    {
        var categories = await _context.ProjectCategories.AsNoTracking().Where(c => c.IsActive).OrderBy(c => c.SortOrder).ToListAsync(cancellationToken);

        var query = _context.Projects.AsNoTracking()
            .Where(p => p.IsPublished)
            .Include(p => p.Tags)
            .Include(p => p.CategoryMaps).ThenInclude(cm => cm.ProjectCategory)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(categorySlug) && !categorySlug.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(p => p.CategoryMaps.Any(cm => cm.ProjectCategory.Slug.ToLower() == categorySlug.ToLower()));
        }

        var projects = await query.OrderBy(p => p.SortOrder).ToListAsync(cancellationToken);

        return new ProjectsResponseDto
        {
            Categories = categories.Select(c => new ProjectCategoryDto
            {
                Slug = c.Slug,
                Label = c.Label,
                SortOrder = c.SortOrder
            }).ToList(),

            Projects = projects.Select(p => new ProjectSummaryDto
            {
                Id = p.Id,
                Slug = p.Slug,
                Title = p.Title,
                ClientName = p.ClientName,
                CategoryBadgeText = p.CategoryBadgeText,
                Timeframe = p.Timeframe,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl,
                ImageAlt = p.ImageAlt,
                IconKey = p.IconKey,
                LiveUrl = p.LiveUrl,
                GithubUrl = p.GithubUrl,
                ArticleUrl = p.ArticleUrl,
                HasCaseStudy = p.HasCaseStudy,
                SortOrder = p.SortOrder,
                Tags = p.Tags.OrderBy(t => t.SortOrder).Select(t => t.TagName).ToList(),
                CategorySlugs = p.CategoryMaps.Select(cm => cm.ProjectCategory.Slug).ToList()
            }).ToList()
        };
    }

    public async Task<ProjectDetailDto?> GetProjectBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        var project = await _context.Projects.AsNoTracking()
            .Include(p => p.Tags)
            .Include(p => p.CategoryMaps).ThenInclude(cm => cm.ProjectCategory)
            .Include(p => p.CaseStudy).ThenInclude(cs => cs!.Highlights)
            .Include(p => p.CaseStudy).ThenInclude(cs => cs!.Technologies)
            .FirstOrDefaultAsync(p => p.Slug.ToLower() == slug.ToLower(), cancellationToken);

        if (project == null) return null;

        return new ProjectDetailDto
        {
            Id = project.Id,
            Slug = project.Slug,
            Title = project.Title,
            ClientName = project.ClientName,
            CategoryBadgeText = project.CategoryBadgeText,
            Timeframe = project.Timeframe,
            ShortDescription = project.ShortDescription,
            ImageUrl = project.ImageUrl,
            ImageAlt = project.ImageAlt,
            IconKey = project.IconKey,
            LiveUrl = project.LiveUrl,
            GithubUrl = project.GithubUrl,
            ArticleUrl = project.ArticleUrl,
            HasCaseStudy = project.HasCaseStudy,
            SortOrder = project.SortOrder,
            Tags = project.Tags.OrderBy(t => t.SortOrder).Select(t => t.TagName).ToList(),
            CategorySlugs = project.CategoryMaps.Select(cm => cm.ProjectCategory.Slug).ToList(),
            CaseStudy = project.CaseStudy != null ? new CaseStudyDto
            {
                Title = project.CaseStudy.Title,
                CategoryLabel = project.CaseStudy.CategoryLabel,
                Year = project.CaseStudy.Year,
                ClientName = project.CaseStudy.ClientName,
                HeroImageUrl = project.CaseStudy.HeroImageUrl,
                Summary = project.CaseStudy.Summary,
                LiveUrl = project.CaseStudy.LiveUrl,
                Highlights = project.CaseStudy.Highlights.OrderBy(h => h.SortOrder).Select(h => h.HighlightText).ToList(),
                Technologies = project.CaseStudy.Technologies.OrderBy(t => t.SortOrder).Select(t => t.Name).ToList()
            } : null
        };
    }

    public async Task<List<ArticleDto>> GetArticlesAsync(CancellationToken cancellationToken = default)
    {
        var articles = await _context.Articles.AsNoTracking()
            .Where(a => a.IsActive)
            .Include(a => a.Tags)
            .OrderBy(a => a.SortOrder)
            .ToListAsync(cancellationToken);

        return articles.Select(a => new ArticleDto
        {
            Id = a.Id,
            Slug = a.Slug,
            Title = a.Title,
            Excerpt = a.Excerpt,
            Category = a.Category,
            PublicationType = a.PublicationType,
            PublishStatus = a.PublishStatus,
            ReadTimeMinutes = a.ReadTimeMinutes,
            ImageUrl = a.ImageUrl,
            ImageAlt = a.ImageAlt,
            LinkedinUrl = a.LinkedinUrl,
            TwitterUrl = a.TwitterUrl,
            FooterAnnotation = a.FooterAnnotation,
            PublishedAt = a.PublishedAt,
            SortOrder = a.SortOrder,
            Tags = a.Tags.OrderBy(t => t.SortOrder).Select(t => t.TagName).ToList()
        }).ToList();
    }

    public async Task<ContactInfoDto?> GetContactInfoAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (profile == null) return null;

        return new ContactInfoDto
        {
            Email = profile.Email,
            Phone = profile.Phone,
            PhoneDisplay = profile.PhoneDisplay,
            WhatsappUrl = profile.WhatsappUrl,
            Location = profile.Location,
            LocationDisplay = profile.LocationDisplay,
            ResponseTime = profile.ResponseTime,
            ResponseGuarantee = profile.ResponseGuarantee,
            EngagementScope = profile.EngagementScope,
            IsAvailable = profile.IsAvailable,
            AvailabilityText = profile.AvailabilityText
        };
    }

    public async Task<NavigationResponseDto> GetNavigationAsync(CancellationToken cancellationToken = default)
    {
        var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var siteSettings = await _context.SiteSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var navItems = await _context.NavItems.AsNoTracking().Where(n => n.IsActive).OrderBy(n => n.SortOrder).ToListAsync(cancellationToken);
        var headerSocials = await _context.SocialLinks.AsNoTracking().Where(s => s.IsActive && s.ShowInHeader).OrderBy(s => s.SortOrder).ToListAsync(cancellationToken);
        var footerSocials = await _context.SocialLinks.AsNoTracking().Where(s => s.IsActive && s.ShowInFooter).OrderBy(s => s.SortOrder).ToListAsync(cancellationToken);

        return new NavigationResponseDto
        {
            Monogram = siteSettings?.Monogram ?? "OA",
            FullName = profile?.FullName ?? "Oluwatobi Adejoro",
            PrimaryTitle = profile?.PrimaryTitle ?? "Senior Fullstack Engineer",
            CvFileUrl = profile?.CvFileUrl ?? "assets/doc/OLUWATOBI_Adejoro_CV(Fullstack).docx",
            CvDownloadName = profile?.CvDownloadName ?? "OLUWATOBI_Adejoro_CV.docx",
            IsAvailable = profile?.IsAvailable ?? true,
            AvailabilityText = profile?.AvailabilityText ?? "Available for opportunities",
            CopyrightText = siteSettings?.CopyrightText ?? "© 2025 Oluwatobi Adejoro. All rights reserved.",
            FooterTagline = siteSettings?.FooterTagline ?? "Engineered with modern web standards, strict typing & high performance.",
            NavItems = navItems.Select(n => new NavItemDto
            {
                Label = n.Label,
                MobileLabel = n.MobileLabel,
                Url = n.Url,
                Icon = n.Icon,
                DataPage = n.DataPage,
                SortOrder = n.SortOrder
            }).ToList(),
            HeaderSocials = headerSocials.Select(s => new SocialLinkDto
            {
                Platform = s.Platform,
                Title = s.Title,
                Url = s.Url,
                Icon = s.Icon,
                ShowInHeader = s.ShowInHeader,
                SortOrder = s.SortOrder
            }).ToList(),
            FooterSocials = footerSocials.Select(s => new SocialLinkDto
            {
                Platform = s.Platform,
                Title = s.Title,
                Url = s.Url,
                Icon = s.Icon,
                ShowInFooter = s.ShowInFooter,
                SortOrder = s.SortOrder
            }).ToList()
        };
    }

    public async Task<PageSettingsDto?> GetPageSettingsAsync(string pageKey, CancellationToken cancellationToken = default)
    {
        var page = await _context.PageSettings.AsNoTracking().FirstOrDefaultAsync(p => p.PageKey.ToLower() == pageKey.ToLower(), cancellationToken);
        if (page == null) return null;

        return new PageSettingsDto
        {
            PageKey = page.PageKey,
            SeoTitle = page.SeoTitle,
            SeoDescription = page.SeoDescription,
            HeroBadgeIcon = page.HeroBadgeIcon,
            HeroBadgeText = page.HeroBadgeText,
            HeroHeading = page.HeroHeading,
            HeroSubtitle = page.HeroSubtitle,
            CalloutHeadline = page.CalloutHeadline,
            CalloutBody = page.CalloutBody,
            CalloutButtonText = page.CalloutButtonText,
            CalloutButtonHref = page.CalloutButtonHref
        };
    }

    public async Task<CreateInquiryResponseDto> SubmitContactInquiryAsync(CreateInquiryRequestDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Message))
        {
            return new CreateInquiryResponseDto
            {
                Success = false,
                Message = "Name, email, and message are required."
            };
        }

        var inquiry = new ContactInquiry
        {
            SenderName = request.Name.Trim(),
            SenderEmail = request.Email.Trim(),
            InquiryType = string.IsNullOrWhiteSpace(request.InquiryType) ? "general" : request.InquiryType.Trim(),
            Message = request.Message.Trim(),
            SubmittedAt = DateTime.UtcNow,
            IsRead = false,
            IsArchived = false
        };

        await _context.ContactInquiries.AddAsync(inquiry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        // Send Email Notification in background (fire-and-forget or awaited)
        _ = _emailService.SendContactNotificationAsync(inquiry.SenderName, inquiry.SenderEmail, inquiry.InquiryType, inquiry.Message, cancellationToken);

        return new CreateInquiryResponseDto
        {
            Success = true,
            Message = "Your message has been received! I will reply within 24 hours.",
            InquiryId = inquiry.Id
        };
    }
}
