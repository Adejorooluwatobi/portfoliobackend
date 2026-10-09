using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Services;

public interface IPortfolioAdminService
{
    // Profile & Identity
    Task<Profile?> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateProfileAsync(UpdateProfileRequestDto dto, CancellationToken cancellationToken = default);
    Task<List<SocialLink>> GetSocialLinksAsync(CancellationToken cancellationToken = default);
    Task<SocialLink> CreateSocialLinkAsync(SocialLinkCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateSocialLinkAsync(Guid id, SocialLinkCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteSocialLinkAsync(Guid id, CancellationToken cancellationToken = default);

    Task<HeroSection?> GetHeroSectionAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateHeroSectionAsync(HeroSectionUpdateDto dto, CancellationToken cancellationToken = default);

    Task<List<PhilosophyCard>> GetPhilosophyCardsAsync(CancellationToken cancellationToken = default);
    Task<PhilosophyCard> CreatePhilosophyCardAsync(PhilosophyCardCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdatePhilosophyCardAsync(Guid id, PhilosophyCardCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeletePhilosophyCardAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<DisciplineCard>> GetDisciplineCardsAsync(CancellationToken cancellationToken = default);
    Task<DisciplineCard> CreateDisciplineCardAsync(DisciplineCardCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateDisciplineCardAsync(Guid id, DisciplineCardCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteDisciplineCardAsync(Guid id, CancellationToken cancellationToken = default);

    // Resume
    Task<List<WorkExperience>> GetExperiencesAsync(CancellationToken cancellationToken = default);
    Task<WorkExperience> CreateExperienceAsync(WorkExperienceCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateExperienceAsync(Guid id, WorkExperienceCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteExperienceAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Education>> GetEducationsAsync(CancellationToken cancellationToken = default);
    Task<Education> CreateEducationAsync(EducationCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateEducationAsync(Guid id, EducationCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteEducationAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<SkillCategory>> GetSkillCategoriesAsync(CancellationToken cancellationToken = default);
    Task<SkillCategory> CreateSkillCategoryAsync(SkillCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateSkillCategoryAsync(Guid id, SkillCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteSkillCategoryAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SkillItem> CreateSkillItemAsync(SkillItemCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateSkillItemAsync(Guid id, SkillItemCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteSkillItemAsync(Guid id, CancellationToken cancellationToken = default);

    // Projects
    Task<List<Project>> GetProjectsAsync(CancellationToken cancellationToken = default);
    Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Project> CreateProjectAsync(ProjectCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateProjectAsync(Guid id, ProjectCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CaseStudy?> GetCaseStudyAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<bool> UpdateCaseStudyAsync(Guid projectId, CaseStudyUpdateDto dto, CancellationToken cancellationToken = default);

    Task<List<ProjectCategory>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task<ProjectCategory> CreateCategoryAsync(ProjectCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateCategoryAsync(Guid id, ProjectCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default);

    // Articles
    Task<List<Article>> GetArticlesAsync(CancellationToken cancellationToken = default);
    Task<Article?> GetArticleByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Article> CreateArticleAsync(ArticleCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateArticleAsync(Guid id, ArticleCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteArticleAsync(Guid id, CancellationToken cancellationToken = default);

    // Inquiries
    Task<List<InquiryItemDto>> GetInquiriesAsync(string? filter = null, CancellationToken cancellationToken = default);
    Task<InquiryItemDto?> GetInquiryByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ToggleInquiryReadAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ToggleInquiryArchivedAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> UpdateInquiryNotesAsync(Guid id, string notes, CancellationToken cancellationToken = default);
    Task<bool> DeleteInquiryAsync(Guid id, CancellationToken cancellationToken = default);
    Task<InquiryStatsDto> GetInquiryStatsAsync(CancellationToken cancellationToken = default);

    // Settings & Navigation
    Task<SiteSettings?> GetSiteSettingsAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateSiteSettingsAsync(SiteSettingsUpdateDto dto, CancellationToken cancellationToken = default);
    Task<List<PageSettings>> GetPageSettingsAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdatePageSettingsAsync(string pageKey, PageSettingsUpdateDto dto, CancellationToken cancellationToken = default);
    Task<List<NavItem>> GetNavItemsAsync(CancellationToken cancellationToken = default);
    Task<NavItem> CreateNavItemAsync(NavItemCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateNavItemAsync(Guid id, NavItemCreateUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteNavItemAsync(Guid id, CancellationToken cancellationToken = default);
}

public class PortfolioAdminService : IPortfolioAdminService
{
    private readonly IAppDbContext _context;

    public PortfolioAdminService(IAppDbContext context)
    {
        _context = context;
    }

    // --- Profile & Identity ---
    public async Task<Profile?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Profiles.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateProfileAsync(UpdateProfileRequestDto dto, CancellationToken cancellationToken = default)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(cancellationToken);
        if (profile == null)
        {
            profile = new Profile();
            await _context.Profiles.AddAsync(profile, cancellationToken);
        }

        profile.FullName = dto.FullName;
        profile.PrimaryTitle = dto.PrimaryTitle;
        profile.SidebarTitle = dto.SidebarTitle;
        profile.AvatarImageUrl = dto.AvatarImageUrl;
        profile.AvatarAltText = dto.AvatarAltText;
        profile.Email = dto.Email;
        profile.Phone = dto.Phone;
        profile.PhoneDisplay = dto.PhoneDisplay;
        profile.WhatsappUrl = dto.WhatsappUrl;
        profile.Location = dto.Location;
        profile.LocationDisplay = dto.LocationDisplay;
        profile.Birthday = dto.Birthday;
        profile.CvFileUrl = dto.CvFileUrl;
        profile.CvDownloadName = dto.CvDownloadName;
        profile.IsAvailable = dto.IsAvailable;
        profile.AvailabilityText = dto.AvailabilityText;
        profile.EmploymentStatus = dto.EmploymentStatus;
        profile.ResponseTime = dto.ResponseTime;
        profile.EngagementScope = dto.EngagementScope;
        profile.ResponseGuarantee = dto.ResponseGuarantee;
        profile.YearsExperience = dto.YearsExperience;
        profile.YearsExperienceSuffix = dto.YearsExperienceSuffix;
        profile.YearsExperienceLabel = dto.YearsExperienceLabel;
        profile.ProjectsCompleted = dto.ProjectsCompleted;
        profile.ProjectsCompletedSuffix = dto.ProjectsCompletedSuffix;
        profile.ProjectsLabel = dto.ProjectsLabel;
        profile.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<SocialLink>> GetSocialLinksAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SocialLinks.OrderBy(s => s.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<SocialLink> CreateSocialLinkAsync(SocialLinkCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var social = new SocialLink
        {
            Platform = dto.Platform,
            Title = dto.Title,
            Url = dto.Url,
            Icon = dto.Icon,
            ShowInHeader = dto.ShowInHeader,
            ShowInFooter = dto.ShowInFooter,
            ShowInSidebar = dto.ShowInSidebar,
            SortOrder = dto.SortOrder,
            IsActive = dto.IsActive
        };

        await _context.SocialLinks.AddAsync(social, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return social;
    }

    public async Task<bool> UpdateSocialLinkAsync(Guid id, SocialLinkCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var social = await _context.SocialLinks.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (social == null) return false;

        social.Platform = dto.Platform;
        social.Title = dto.Title;
        social.Url = dto.Url;
        social.Icon = dto.Icon;
        social.ShowInHeader = dto.ShowInHeader;
        social.ShowInFooter = dto.ShowInFooter;
        social.ShowInSidebar = dto.ShowInSidebar;
        social.SortOrder = dto.SortOrder;
        social.IsActive = dto.IsActive;
        social.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteSocialLinkAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var social = await _context.SocialLinks.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (social == null) return false;

        _context.SocialLinks.Remove(social);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<HeroSection?> GetHeroSectionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.HeroSections.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateHeroSectionAsync(HeroSectionUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var hero = await _context.HeroSections.FirstOrDefaultAsync(cancellationToken);
        if (hero == null)
        {
            hero = new HeroSection();
            await _context.HeroSections.AddAsync(hero, cancellationToken);
        }

        hero.CategoryBadgeText = dto.CategoryBadgeText;
        hero.CategoryBadgeIcon = dto.CategoryBadgeIcon;
        hero.Headline = dto.Headline;
        hero.BioLead = dto.BioLead;
        hero.BioFrontend = dto.BioFrontend;
        hero.BioBackend = dto.BioBackend;
        hero.PrimaryCtaText = dto.PrimaryCtaText;
        hero.PrimaryCtaUrl = dto.PrimaryCtaUrl;
        hero.PrimaryCtaIcon = dto.PrimaryCtaIcon;
        hero.SecondaryCtaText = dto.SecondaryCtaText;
        hero.SecondaryCtaUrl = dto.SecondaryCtaUrl;
        hero.SecondaryCtaIcon = dto.SecondaryCtaIcon;
        hero.TertiaryCtaText = dto.TertiaryCtaText;
        hero.TertiaryCtaUrl = dto.TertiaryCtaUrl;
        hero.TertiaryCtaIcon = dto.TertiaryCtaIcon;
        hero.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<PhilosophyCard>> GetPhilosophyCardsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.PhilosophyCards.OrderBy(p => p.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<PhilosophyCard> CreatePhilosophyCardAsync(PhilosophyCardCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var card = new PhilosophyCard
        {
            Icon = dto.Icon,
            Title = dto.Title,
            Description = dto.Description,
            SortOrder = dto.SortOrder
        };

        await _context.PhilosophyCards.AddAsync(card, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return card;
    }

    public async Task<bool> UpdatePhilosophyCardAsync(Guid id, PhilosophyCardCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var card = await _context.PhilosophyCards.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (card == null) return false;

        card.Icon = dto.Icon;
        card.Title = dto.Title;
        card.Description = dto.Description;
        card.SortOrder = dto.SortOrder;
        card.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeletePhilosophyCardAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var card = await _context.PhilosophyCards.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (card == null) return false;

        _context.PhilosophyCards.Remove(card);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<DisciplineCard>> GetDisciplineCardsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.DisciplineCards.Include(d => d.Tags).OrderBy(d => d.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<DisciplineCard> CreateDisciplineCardAsync(DisciplineCardCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var card = new DisciplineCard
        {
            IndexTag = dto.IndexTag,
            Icon = dto.Icon,
            Title = dto.Title,
            Description = dto.Description,
            SortOrder = dto.SortOrder,
            AccentColor = string.IsNullOrWhiteSpace(dto.AccentColor) ? "#8b5cf6" : dto.AccentColor,
            Tags = dto.Tags.Select((t, i) => new DisciplineCardTag { TagName = t, SortOrder = i + 1 }).ToList()
        };

        await _context.DisciplineCards.AddAsync(card, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return card;
    }

    public async Task<bool> UpdateDisciplineCardAsync(Guid id, DisciplineCardCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var card = await _context.DisciplineCards.Include(d => d.Tags).FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (card == null) return false;

        card.IndexTag = dto.IndexTag;
        card.Icon = dto.Icon;
        card.Title = dto.Title;
        card.Description = dto.Description;
        card.SortOrder = dto.SortOrder;
        card.AccentColor = string.IsNullOrWhiteSpace(dto.AccentColor) ? (card.AccentColor ?? "#8b5cf6") : dto.AccentColor;
        card.UpdatedAt = DateTime.UtcNow;

        _context.DisciplineCardTags.RemoveRange(card.Tags);
        var newTags = dto.Tags.Select((t, i) => new DisciplineCardTag { DisciplineCardId = id, TagName = t, SortOrder = i + 1 }).ToList();
        await _context.DisciplineCardTags.AddRangeAsync(newTags, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteDisciplineCardAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var card = await _context.DisciplineCards.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (card == null) return false;

        _context.DisciplineCards.Remove(card);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    // --- Resume ---
    public async Task<List<WorkExperience>> GetExperiencesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.WorkExperiences.Include(w => w.Technologies).OrderBy(w => w.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<WorkExperience> CreateExperienceAsync(WorkExperienceCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var exp = new WorkExperience
        {
            JobTitle = dto.JobTitle,
            CompanyName = dto.CompanyName,
            EmploymentType = dto.EmploymentType,
            DateRange = dto.DateRange,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsCurrent = dto.IsCurrent,
            Description = dto.Description,
            AccentVariant = dto.AccentVariant,
            SortOrder = dto.SortOrder,
            Technologies = dto.Technologies.Select((t, i) => new ExperienceTechnology { Name = t, SortOrder = i + 1 }).ToList()
        };

        await _context.WorkExperiences.AddAsync(exp, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return exp;
    }

    public async Task<bool> UpdateExperienceAsync(Guid id, WorkExperienceCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var exp = await _context.WorkExperiences.Include(w => w.Technologies).FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        if (exp == null) return false;

        exp.JobTitle = dto.JobTitle;
        exp.CompanyName = dto.CompanyName;
        exp.EmploymentType = dto.EmploymentType;
        exp.DateRange = dto.DateRange;
        exp.StartDate = dto.StartDate;
        exp.EndDate = dto.EndDate;
        exp.IsCurrent = dto.IsCurrent;
        exp.Description = dto.Description;
        exp.AccentVariant = dto.AccentVariant;
        exp.SortOrder = dto.SortOrder;
        exp.UpdatedAt = DateTime.UtcNow;

        _context.ExperienceTechnologies.RemoveRange(exp.Technologies);
        var newTechs = dto.Technologies.Select((t, i) => new ExperienceTechnology { WorkExperienceId = id, Name = t, SortOrder = i + 1 }).ToList();
        await _context.ExperienceTechnologies.AddRangeAsync(newTechs, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteExperienceAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exp = await _context.WorkExperiences.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        if (exp == null) return false;

        _context.WorkExperiences.Remove(exp);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<Education>> GetEducationsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Educations.OrderBy(e => e.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<Education> CreateEducationAsync(EducationCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var edu = new Education
        {
            DegreeTitle = dto.DegreeTitle,
            InstitutionName = dto.InstitutionName,
            Location = dto.Location,
            DateRange = dto.DateRange,
            Icon = dto.Icon,
            Description = dto.Description,
            CredentialType = dto.CredentialType,
            SortOrder = dto.SortOrder
        };

        await _context.Educations.AddAsync(edu, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return edu;
    }

    public async Task<bool> UpdateEducationAsync(Guid id, EducationCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var edu = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (edu == null) return false;

        edu.DegreeTitle = dto.DegreeTitle;
        edu.InstitutionName = dto.InstitutionName;
        edu.Location = dto.Location;
        edu.DateRange = dto.DateRange;
        edu.Icon = dto.Icon;
        edu.Description = dto.Description;
        edu.CredentialType = dto.CredentialType;
        edu.SortOrder = dto.SortOrder;
        edu.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteEducationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var edu = await _context.Educations.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (edu == null) return false;

        _context.Educations.Remove(edu);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<SkillCategory>> GetSkillCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SkillCategories.Include(s => s.Skills).OrderBy(s => s.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<SkillCategory> CreateSkillCategoryAsync(SkillCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var cat = new SkillCategory
        {
            Title = dto.Title,
            Subtitle = dto.Subtitle,
            Icon = dto.Icon,
            AccentColorToken = dto.AccentColorToken,
            SortOrder = dto.SortOrder
        };

        await _context.SkillCategories.AddAsync(cat, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cat;
    }

    public async Task<bool> UpdateSkillCategoryAsync(Guid id, SkillCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var cat = await _context.SkillCategories.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (cat == null) return false;

        cat.Title = dto.Title;
        cat.Subtitle = dto.Subtitle;
        cat.Icon = dto.Icon;
        cat.AccentColorToken = dto.AccentColorToken;
        cat.SortOrder = dto.SortOrder;
        cat.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteSkillCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cat = await _context.SkillCategories.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (cat == null) return false;

        _context.SkillCategories.Remove(cat);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<SkillItem> CreateSkillItemAsync(SkillItemCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var item = new SkillItem
        {
            SkillCategoryId = dto.SkillCategoryId,
            Name = dto.Name,
            ProficiencyPercent = dto.ProficiencyPercent,
            IsPrimary = dto.IsPrimary,
            SortOrder = dto.SortOrder
        };

        await _context.SkillItems.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<bool> UpdateSkillItemAsync(Guid id, SkillItemCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var item = await _context.SkillItems.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (item == null) return false;

        item.Name = dto.Name;
        item.ProficiencyPercent = dto.ProficiencyPercent;
        item.IsPrimary = dto.IsPrimary;
        item.SortOrder = dto.SortOrder;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteSkillItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.SkillItems.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (item == null) return false;

        _context.SkillItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    // --- Projects ---
    public async Task<List<Project>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Tags)
            .Include(p => p.CategoryMaps).ThenInclude(cm => cm.ProjectCategory)
            .OrderBy(p => p.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Include(p => p.Tags)
            .Include(p => p.CategoryMaps).ThenInclude(cm => cm.ProjectCategory)
            .Include(p => p.CaseStudy).ThenInclude(cs => cs!.Highlights)
            .Include(p => p.CaseStudy).ThenInclude(cs => cs!.Technologies)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Project> CreateProjectAsync(ProjectCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var project = new Project
        {
            Slug = dto.Slug.Trim().ToLower(),
            Title = dto.Title,
            ClientName = dto.ClientName,
            CategoryBadgeText = dto.CategoryBadgeText,
            Timeframe = dto.Timeframe,
            ShortDescription = dto.ShortDescription,
            ImageUrl = dto.ImageUrl,
            ImageAlt = dto.ImageAlt,
            IconKey = dto.IconKey,
            LiveUrl = dto.LiveUrl,
            GithubUrl = dto.GithubUrl,
            ArticleUrl = dto.ArticleUrl,
            HasCaseStudy = dto.HasCaseStudy,
            IsPublished = dto.IsPublished,
            SortOrder = dto.SortOrder,
            Tags = dto.Tags.Select((t, i) => new ProjectTag { TagName = t, SortOrder = i + 1 }).ToList(),
            CategoryMaps = dto.CategoryIds.Select(catId => new ProjectCategoryMap { ProjectCategoryId = catId }).ToList()
        };

        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return project;
    }

    public async Task<bool> UpdateProjectAsync(Guid id, ProjectCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var project = await _context.Projects
            .Include(p => p.Tags)
            .Include(p => p.CategoryMaps)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (project == null) return false;

        project.Slug = dto.Slug.Trim().ToLower();
        project.Title = dto.Title;
        project.ClientName = dto.ClientName;
        project.CategoryBadgeText = dto.CategoryBadgeText;
        project.Timeframe = dto.Timeframe;
        project.ShortDescription = dto.ShortDescription;
        project.ImageUrl = dto.ImageUrl;
        project.ImageAlt = dto.ImageAlt;
        project.IconKey = dto.IconKey;
        project.LiveUrl = dto.LiveUrl;
        project.GithubUrl = dto.GithubUrl;
        project.ArticleUrl = dto.ArticleUrl;
        project.HasCaseStudy = dto.HasCaseStudy;
        project.IsPublished = dto.IsPublished;
        project.SortOrder = dto.SortOrder;
        project.UpdatedAt = DateTime.UtcNow;

        _context.ProjectTags.RemoveRange(project.Tags);
        _context.ProjectCategoryMaps.RemoveRange(project.CategoryMaps);

        var newTags = dto.Tags.Select((t, i) => new ProjectTag { ProjectId = id, TagName = t, SortOrder = i + 1 }).ToList();
        var newCatMaps = dto.CategoryIds.Select(catId => new ProjectCategoryMap { ProjectId = id, ProjectCategoryId = catId }).ToList();

        await _context.ProjectTags.AddRangeAsync(newTags, cancellationToken);
        await _context.ProjectCategoryMaps.AddRangeAsync(newCatMaps, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (project == null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<CaseStudy?> GetCaseStudyAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.CaseStudies
            .Include(cs => cs.Highlights)
            .Include(cs => cs.Technologies)
            .FirstOrDefaultAsync(cs => cs.ProjectId == projectId, cancellationToken);
    }

    public async Task<bool> UpdateCaseStudyAsync(Guid projectId, CaseStudyUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var cs = await _context.CaseStudies
            .Include(c => c.Highlights)
            .Include(c => c.Technologies)
            .FirstOrDefaultAsync(c => c.ProjectId == projectId, cancellationToken);

        if (cs == null)
        {
            cs = new CaseStudy { ProjectId = projectId };
            await _context.CaseStudies.AddAsync(cs, cancellationToken);
        }

        cs.Title = dto.Title;
        cs.CategoryLabel = dto.CategoryLabel;
        cs.Year = dto.Year;
        cs.ClientName = dto.ClientName;
        cs.HeroImageUrl = dto.HeroImageUrl;
        cs.Summary = dto.Summary;
        cs.LiveUrl = dto.LiveUrl;
        cs.UpdatedAt = DateTime.UtcNow;

        _context.CaseStudyHighlights.RemoveRange(cs.Highlights);
        _context.CaseStudyTechnologies.RemoveRange(cs.Technologies);

        var newHighlights = dto.Highlights.Select((h, i) => new CaseStudyHighlight { CaseStudyId = cs.Id, HighlightText = h, SortOrder = i + 1 }).ToList();
        var newTechs = dto.Technologies.Select((t, i) => new CaseStudyTechnology { CaseStudyId = cs.Id, Name = t, SortOrder = i + 1 }).ToList();

        await _context.CaseStudyHighlights.AddRangeAsync(newHighlights, cancellationToken);
        await _context.CaseStudyTechnologies.AddRangeAsync(newTechs, cancellationToken);

        // Also mark project HasCaseStudy = true
        var proj = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
        if (proj != null) proj.HasCaseStudy = true;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<ProjectCategory>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ProjectCategories.OrderBy(c => c.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<ProjectCategory> CreateCategoryAsync(ProjectCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var cat = new ProjectCategory
        {
            Slug = dto.Slug.Trim().ToLower(),
            Label = dto.Label,
            SortOrder = dto.SortOrder,
            IsActive = dto.IsActive
        };

        await _context.ProjectCategories.AddAsync(cat, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cat;
    }

    public async Task<bool> UpdateCategoryAsync(Guid id, ProjectCategoryCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var cat = await _context.ProjectCategories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (cat == null) return false;

        cat.Slug = dto.Slug.Trim().ToLower();
        cat.Label = dto.Label;
        cat.SortOrder = dto.SortOrder;
        cat.IsActive = dto.IsActive;
        cat.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cat = await _context.ProjectCategories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (cat == null) return false;

        // Protect the root 'all' category from deletion
        if (string.Equals(cat.Slug, "all", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var maps = await _context.ProjectCategoryMaps.Where(m => m.ProjectCategoryId == id).ToListAsync(cancellationToken);
        if (maps.Count > 0)
        {
            _context.ProjectCategoryMaps.RemoveRange(maps);
        }

        _context.ProjectCategories.Remove(cat);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    // --- Articles ---
    public async Task<List<Article>> GetArticlesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Articles.Include(a => a.Tags).Include(a => a.Links).OrderBy(a => a.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<Article?> GetArticleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Articles.Include(a => a.Tags).Include(a => a.Links).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Article> CreateArticleAsync(ArticleCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var article = new Article
        {
            Slug = dto.Slug.Trim().ToLower(),
            Title = dto.Title,
            Excerpt = dto.Excerpt,
            Category = dto.Category,
            PublicationType = dto.PublicationType,
            PublishStatus = dto.PublishStatus,
            ReadTimeMinutes = dto.ReadTimeMinutes,
            ImageUrl = dto.ImageUrl,
            ImageAlt = dto.ImageAlt,
            LinkedinUrl = dto.LinkedinUrl,
            TwitterUrl = dto.TwitterUrl,
            FooterAnnotation = dto.FooterAnnotation,
            PublishedAt = dto.PublishedAt ?? DateTime.UtcNow,
            IsActive = dto.IsActive,
            SortOrder = dto.SortOrder,
            Tags = (dto.Tags != null && dto.Tags.Any()) 
                ? dto.Tags.Select((t, i) => new ArticleTag { TagName = t, SortOrder = i + 1 }).ToList() 
                : new List<ArticleTag>(),
            Links = (dto.Links != null && dto.Links.Any())
                ? dto.Links.Select((l, i) => new ArticleLink { Title = l.Title, Url = l.Url, Icon = l.Icon, SortOrder = l.SortOrder > 0 ? l.SortOrder : i + 1 }).ToList()
                : new List<ArticleLink>()
        };

        if (!article.Links.Any())
        {
            if (!string.IsNullOrWhiteSpace(dto.LinkedinUrl))
            {
                article.Links.Add(new ArticleLink { Title = "LinkedIn", Url = dto.LinkedinUrl, Icon = "linkedin", SortOrder = 1 });
            }
            if (!string.IsNullOrWhiteSpace(dto.TwitterUrl))
            {
                article.Links.Add(new ArticleLink { Title = "X / Twitter", Url = dto.TwitterUrl, Icon = "twitter", SortOrder = 2 });
            }
        }

        await _context.Articles.AddAsync(article, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return article;
    }

    public async Task<bool> UpdateArticleAsync(Guid id, ArticleCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var article = await _context.Articles.Include(a => a.Tags).Include(a => a.Links).FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        if (article == null) return false;

        article.Slug = string.IsNullOrWhiteSpace(dto.Slug) ? (article.Slug ?? Guid.NewGuid().ToString()) : dto.Slug.Trim().ToLower();
        article.Title = dto.Title;
        article.Excerpt = dto.Excerpt;
        article.Category = dto.Category;
        article.PublicationType = dto.PublicationType;
        article.PublishStatus = dto.PublishStatus;
        article.ReadTimeMinutes = dto.ReadTimeMinutes;
        article.ImageUrl = dto.ImageUrl;
        article.ImageAlt = dto.ImageAlt;
        article.LinkedinUrl = dto.LinkedinUrl;
        article.TwitterUrl = dto.TwitterUrl;
        article.FooterAnnotation = dto.FooterAnnotation;
        article.PublishedAt = dto.PublishedAt;
        article.IsActive = dto.IsActive;
        article.SortOrder = dto.SortOrder;
        article.UpdatedAt = DateTime.UtcNow;

        _context.ArticleTags.RemoveRange(article.Tags);
        if (dto.Tags != null && dto.Tags.Any())
        {
            var newTags = dto.Tags.Select((t, i) => new ArticleTag { ArticleId = id, TagName = t, SortOrder = i + 1 }).ToList();
            await _context.ArticleTags.AddRangeAsync(newTags, cancellationToken);
        }

        _context.ArticleLinks.RemoveRange(article.Links);
        var linksToSave = new List<ArticleLink>();
        if (dto.Links != null && dto.Links.Any())
        {
            linksToSave = dto.Links.Select((l, i) => new ArticleLink { ArticleId = id, Title = l.Title, Url = l.Url, Icon = l.Icon, SortOrder = l.SortOrder > 0 ? l.SortOrder : i + 1 }).ToList();
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(dto.LinkedinUrl))
            {
                linksToSave.Add(new ArticleLink { ArticleId = id, Title = "LinkedIn", Url = dto.LinkedinUrl, Icon = "linkedin", SortOrder = 1 });
            }
            if (!string.IsNullOrWhiteSpace(dto.TwitterUrl))
            {
                linksToSave.Add(new ArticleLink { ArticleId = id, Title = "X / Twitter", Url = dto.TwitterUrl, Icon = "twitter", SortOrder = 2 });
            }
        }
        if (linksToSave.Any())
        {
            await _context.ArticleLinks.AddRangeAsync(linksToSave, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteArticleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        if (article == null) return false;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    // --- Inquiries ---
    public async Task<List<InquiryItemDto>> GetInquiriesAsync(string? filter = null, CancellationToken cancellationToken = default)
    {
        var query = _context.ContactInquiries.AsNoTracking().AsQueryable();

        if (filter?.ToLower() == "unread")
        {
            query = query.Where(i => !i.IsRead && !i.IsArchived);
        }
        else if (filter?.ToLower() == "archived")
        {
            query = query.Where(i => i.IsArchived);
        }
        else
        {
            query = query.Where(i => !i.IsArchived);
        }

        var list = await query.OrderByDescending(i => i.SubmittedAt).ToListAsync(cancellationToken);

        return list.Select(i => new InquiryItemDto
        {
            Id = i.Id,
            SenderName = i.SenderName,
            SenderEmail = i.SenderEmail,
            InquiryType = i.InquiryType,
            Message = i.Message,
            IsRead = i.IsRead,
            IsArchived = i.IsArchived,
            AdminNotes = i.AdminNotes,
            SubmittedAt = i.SubmittedAt,
            ReadAt = i.ReadAt
        }).ToList();
    }

    public async Task<InquiryItemDto?> GetInquiryByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var i = await _context.ContactInquiries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (i == null) return null;

        if (!i.IsRead)
        {
            i.IsRead = true;
            i.ReadAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return new InquiryItemDto
        {
            Id = i.Id,
            SenderName = i.SenderName,
            SenderEmail = i.SenderEmail,
            InquiryType = i.InquiryType,
            Message = i.Message,
            IsRead = i.IsRead,
            IsArchived = i.IsArchived,
            AdminNotes = i.AdminNotes,
            SubmittedAt = i.SubmittedAt,
            ReadAt = i.ReadAt
        };
    }

    public async Task<bool> ToggleInquiryReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var i = await _context.ContactInquiries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (i == null) return false;

        i.IsRead = !i.IsRead;
        i.ReadAt = i.IsRead ? DateTime.UtcNow : null;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ToggleInquiryArchivedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var i = await _context.ContactInquiries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (i == null) return false;

        i.IsArchived = !i.IsArchived;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateInquiryNotesAsync(Guid id, string notes, CancellationToken cancellationToken = default)
    {
        var i = await _context.ContactInquiries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (i == null) return false;

        i.AdminNotes = notes;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteInquiryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var i = await _context.ContactInquiries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (i == null) return false;

        _context.ContactInquiries.Remove(i);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<InquiryStatsDto> GetInquiryStatsAsync(CancellationToken cancellationToken = default)
    {
        var total = await _context.ContactInquiries.CountAsync(cancellationToken);
        var unread = await _context.ContactInquiries.CountAsync(i => !i.IsRead && !i.IsArchived, cancellationToken);
        var archived = await _context.ContactInquiries.CountAsync(i => i.IsArchived, cancellationToken);
        var today = await _context.ContactInquiries.CountAsync(i => i.SubmittedAt >= DateTime.UtcNow.Date, cancellationToken);

        return new InquiryStatsDto
        {
            TotalCount = total,
            UnreadCount = unread,
            ArchivedCount = archived,
            TodayCount = today
        };
    }

    // --- Settings & Navigation ---
    public async Task<SiteSettings?> GetSiteSettingsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SiteSettings.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> UpdateSiteSettingsAsync(SiteSettingsUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var s = await _context.SiteSettings.FirstOrDefaultAsync(cancellationToken);
        if (s == null)
        {
            s = new SiteSettings();
            await _context.SiteSettings.AddAsync(s, cancellationToken);
        }

        s.SiteTitle = dto.SiteTitle;
        s.Monogram = dto.Monogram;
        s.FaviconUrl = dto.FaviconUrl;
        s.DefaultTheme = dto.DefaultTheme;
        s.CopyrightText = dto.CopyrightText;
        s.FooterTagline = dto.FooterTagline;
        s.MetaDescription = dto.MetaDescription;
        s.FormspreeEndpoint = dto.FormspreeEndpoint;
        s.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<PageSettings>> GetPageSettingsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.PageSettings.ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdatePageSettingsAsync(string pageKey, PageSettingsUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var page = await _context.PageSettings.FirstOrDefaultAsync(p => p.PageKey.ToLower() == pageKey.ToLower(), cancellationToken);
        if (page == null)
        {
            page = new PageSettings { PageKey = pageKey.ToLower() };
            await _context.PageSettings.AddAsync(page, cancellationToken);
        }

        page.SeoTitle = dto.SeoTitle;
        page.SeoDescription = dto.SeoDescription;
        page.HeroBadgeIcon = dto.HeroBadgeIcon;
        page.HeroBadgeText = dto.HeroBadgeText;
        page.HeroHeading = dto.HeroHeading;
        page.HeroSubtitle = dto.HeroSubtitle;
        page.CalloutHeadline = dto.CalloutHeadline;
        page.CalloutBody = dto.CalloutBody;
        page.CalloutButtonText = dto.CalloutButtonText;
        page.CalloutButtonHref = dto.CalloutButtonHref;
        page.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<NavItem>> GetNavItemsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.NavItems.OrderBy(n => n.SortOrder).ToListAsync(cancellationToken);
    }

    public async Task<NavItem> CreateNavItemAsync(NavItemCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var item = new NavItem
        {
            Label = dto.Label,
            MobileLabel = dto.MobileLabel,
            Url = dto.Url,
            Icon = dto.Icon,
            DataPage = dto.DataPage,
            SortOrder = dto.SortOrder,
            IsActive = dto.IsActive
        };

        await _context.NavItems.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<bool> UpdateNavItemAsync(Guid id, NavItemCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var item = await _context.NavItems.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        if (item == null) return false;

        item.Label = dto.Label;
        item.MobileLabel = dto.MobileLabel;
        item.Url = dto.Url;
        item.Icon = dto.Icon;
        item.DataPage = dto.DataPage;
        item.SortOrder = dto.SortOrder;
        item.IsActive = dto.IsActive;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteNavItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var item = await _context.NavItems.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        if (item == null) return false;

        _context.NavItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
