using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<AdminUser> AdminUsers { get; }
    DbSet<SiteSettings> SiteSettings { get; }
    DbSet<Profile> Profiles { get; }
    DbSet<SocialLink> SocialLinks { get; }
    DbSet<HeroSection> HeroSections { get; }
    DbSet<PhilosophyCard> PhilosophyCards { get; }
    DbSet<DisciplineCard> DisciplineCards { get; }
    DbSet<DisciplineCardTag> DisciplineCardTags { get; }
    DbSet<WorkExperience> WorkExperiences { get; }
    DbSet<ExperienceTechnology> ExperienceTechnologies { get; }
    DbSet<Education> Educations { get; }
    DbSet<SkillCategory> SkillCategories { get; }
    DbSet<SkillItem> SkillItems { get; }
    DbSet<ProjectCategory> ProjectCategories { get; }
    DbSet<Project> Projects { get; }
    DbSet<ProjectTag> ProjectTags { get; }
    DbSet<ProjectCategoryMap> ProjectCategoryMaps { get; }
    DbSet<CaseStudy> CaseStudies { get; }
    DbSet<CaseStudyHighlight> CaseStudyHighlights { get; }
    DbSet<CaseStudyTechnology> CaseStudyTechnologies { get; }
    DbSet<Article> Articles { get; }
    DbSet<ArticleTag> ArticleTags { get; }
    DbSet<ContactInquiry> ContactInquiries { get; }
    DbSet<PageSettings> PageSettings { get; }
    DbSet<NavItem> NavItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
