using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<SiteSettings> SiteSettings => Set<SiteSettings>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<HeroSection> HeroSections => Set<HeroSection>();
    public DbSet<PhilosophyCard> PhilosophyCards => Set<PhilosophyCard>();
    public DbSet<DisciplineCard> DisciplineCards => Set<DisciplineCard>();
    public DbSet<DisciplineCardTag> DisciplineCardTags => Set<DisciplineCardTag>();
    public DbSet<WorkExperience> WorkExperiences => Set<WorkExperience>();
    public DbSet<ExperienceTechnology> ExperienceTechnologies => Set<ExperienceTechnology>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<SkillCategory> SkillCategories => Set<SkillCategory>();
    public DbSet<SkillItem> SkillItems => Set<SkillItem>();
    public DbSet<ProjectCategory> ProjectCategories => Set<ProjectCategory>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectTag> ProjectTags => Set<ProjectTag>();
    public DbSet<ProjectCategoryMap> ProjectCategoryMaps => Set<ProjectCategoryMap>();
    public DbSet<CaseStudy> CaseStudies => Set<CaseStudy>();
    public DbSet<CaseStudyHighlight> CaseStudyHighlights => Set<CaseStudyHighlight>();
    public DbSet<CaseStudyTechnology> CaseStudyTechnologies => Set<CaseStudyTechnology>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    public DbSet<ArticleLink> ArticleLinks => Set<ArticleLink>();
    public DbSet<ContactInquiry> ContactInquiries => Set<ContactInquiry>();
    public DbSet<PageSettings> PageSettings => Set<PageSettings>();
    public DbSet<NavItem> NavItems => Set<NavItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AdminUser
        modelBuilder.Entity<AdminUser>(b =>
        {
            b.ToTable("admin_users");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.Email).HasMaxLength(256).IsRequired();
            b.Property(x => x.FullName).HasMaxLength(100).IsRequired();
        });

        // SiteSettings
        modelBuilder.Entity<SiteSettings>(b =>
        {
            b.ToTable("site_settings");
            b.HasKey(x => x.Id);
        });

        // Profile
        modelBuilder.Entity<Profile>(b =>
        {
            b.ToTable("profiles");
            b.HasKey(x => x.Id);
        });

        // SocialLink
        modelBuilder.Entity<SocialLink>(b =>
        {
            b.ToTable("social_links");
            b.HasKey(x => x.Id);
        });

        // HeroSection
        modelBuilder.Entity<HeroSection>(b =>
        {
            b.ToTable("hero_sections");
            b.HasKey(x => x.Id);
        });

        // PhilosophyCard
        modelBuilder.Entity<PhilosophyCard>(b =>
        {
            b.ToTable("philosophy_cards");
            b.HasKey(x => x.Id);
            b.Property(x => x.AccentColor).HasMaxLength(30).HasDefaultValue("#8b5cf6");
        });

        // DisciplineCard & Tags
        modelBuilder.Entity<DisciplineCard>(b =>
        {
            b.ToTable("discipline_cards");
            b.HasKey(x => x.Id);
            b.HasMany(x => x.Tags)
                .WithOne(x => x.DisciplineCard)
                .HasForeignKey(x => x.DisciplineCardId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DisciplineCardTag>(b =>
        {
            b.ToTable("discipline_card_tags");
            b.HasKey(x => x.Id);
        });

        // WorkExperience & Technologies
        modelBuilder.Entity<WorkExperience>(b =>
        {
            b.ToTable("work_experiences");
            b.HasKey(x => x.Id);
            b.HasMany(x => x.Technologies)
                .WithOne(x => x.WorkExperience)
                .HasForeignKey(x => x.WorkExperienceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ExperienceTechnology>(b =>
        {
            b.ToTable("experience_technologies");
            b.HasKey(x => x.Id);
        });

        // Education
        modelBuilder.Entity<Education>(b =>
        {
            b.ToTable("educations");
            b.HasKey(x => x.Id);
        });

        // SkillCategory & SkillItems
        modelBuilder.Entity<SkillCategory>(b =>
        {
            b.ToTable("skill_categories");
            b.HasKey(x => x.Id);
            b.HasMany(x => x.Skills)
                .WithOne(x => x.SkillCategory)
                .HasForeignKey(x => x.SkillCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SkillItem>(b =>
        {
            b.ToTable("skill_items");
            b.HasKey(x => x.Id);
        });

        // Project Category & Mapping
        modelBuilder.Entity<ProjectCategory>(b =>
        {
            b.ToTable("project_categories");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Slug).IsUnique();
        });

        modelBuilder.Entity<Project>(b =>
        {
            b.ToTable("projects");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Slug).IsUnique();
            b.HasMany(x => x.Tags)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.CaseStudy)
                .WithOne(x => x.Project)
                .HasForeignKey<CaseStudy>(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProjectTag>(b =>
        {
            b.ToTable("project_tags");
            b.HasKey(x => x.Id);
        });

        modelBuilder.Entity<ProjectCategoryMap>(b =>
        {
            b.ToTable("project_category_maps");
            b.HasKey(x => new { x.ProjectId, x.ProjectCategoryId });
            b.HasOne(x => x.Project)
                .WithMany(x => x.CategoryMaps)
                .HasForeignKey(x => x.ProjectId);
            b.HasOne(x => x.ProjectCategory)
                .WithMany(x => x.ProjectMaps)
                .HasForeignKey(x => x.ProjectCategoryId);
        });

        // CaseStudy & Highlights / Technologies
        modelBuilder.Entity<CaseStudy>(b =>
        {
            b.ToTable("case_studies");
            b.HasKey(x => x.Id);
            b.HasMany(x => x.Highlights)
                .WithOne(x => x.CaseStudy)
                .HasForeignKey(x => x.CaseStudyId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Technologies)
                .WithOne(x => x.CaseStudy)
                .HasForeignKey(x => x.CaseStudyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CaseStudyHighlight>(b =>
        {
            b.ToTable("case_study_highlights");
            b.HasKey(x => x.Id);
        });

        modelBuilder.Entity<CaseStudyTechnology>(b =>
        {
            b.ToTable("case_study_technologies");
            b.HasKey(x => x.Id);
        });

        // Article & Tags & Links
        modelBuilder.Entity<Article>(b =>
        {
            b.ToTable("articles");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Slug).IsUnique();
            b.HasMany(x => x.Tags)
                .WithOne(x => x.Article)
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.Links)
                .WithOne(x => x.Article)
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ArticleTag>(b =>
        {
            b.ToTable("article_tags");
            b.HasKey(x => x.Id);
        });

        modelBuilder.Entity<ArticleLink>(b =>
        {
            b.ToTable("article_links");
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).HasMaxLength(150).IsRequired();
            b.Property(x => x.Url).HasMaxLength(1000).IsRequired();
            b.Property(x => x.Icon).HasMaxLength(100);
        });

        // ContactInquiry
        modelBuilder.Entity<ContactInquiry>(b =>
        {
            b.ToTable("contact_inquiries");
            b.HasKey(x => x.Id);
        });

        // PageSettings
        modelBuilder.Entity<PageSettings>(b =>
        {
            b.ToTable("page_settings");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.PageKey).IsUnique();
        });

        // NavItem
        modelBuilder.Entity<NavItem>(b =>
        {
            b.ToTable("nav_items");
            b.HasKey(x => x.Id);
        });
    }
}
