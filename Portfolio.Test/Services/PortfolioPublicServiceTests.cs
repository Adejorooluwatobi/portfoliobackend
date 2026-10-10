using FluentAssertions;
using Moq;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;
using Portfolio.Domain.Entities;
using Portfolio.Test.Common;
using Xunit;

namespace Portfolio.Test.Services;

public class PortfolioPublicServiceTests
{
    private readonly Mock<IEmailService> _mockEmail;

    public PortfolioPublicServiceTests()
    {
        _mockEmail = new Mock<IEmailService>();
        _mockEmail.Setup(e => e.SendContactNotificationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }

    [Fact]
    public async Task GetProfileAsync_ReturnsFullProfile_WhenProfileExists()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var profile = new Profile
        {
            FullName = "Oluwatobi Adejoro",
            PrimaryTitle = "Senior Fullstack Engineer",
            Email = "Adejorotgold1@yahoo.com",
            YearsExperience = 4,
            ProjectsCompleted = 25,
            IsAvailable = true
        };
        var settings = new SiteSettings { SiteTitle = "Oluwatobi's Portfolio", Monogram = "OA" };
        var social = new SocialLink { Platform = "linkedin", Url = "https://linkedin.com", ShowInHeader = true, IsActive = true };

        await context.Profiles.AddAsync(profile);
        await context.SiteSettings.AddAsync(settings);
        await context.SocialLinks.AddAsync(social);
        await context.SaveChangesAsync();

        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var result = await service.GetProfileAsync();

        // Assert
        result.Should().NotBeNull();
        result!.FullName.Should().Be("Oluwatobi Adejoro");
        result.PrimaryTitle.Should().Be("Senior Fullstack Engineer");
        result.Email.Should().Be("Adejorotgold1@yahoo.com");
        result.SiteSettings.Monogram.Should().Be("OA");
        result.SocialLinks.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetResumeAsync_ReturnsTimelineAndSkills()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var exp = new WorkExperience
        {
            JobTitle = "Senior Developer",
            CompanyName = "Cytech Consult",
            DateRange = "2024 — Present",
            SortOrder = 1,
            Technologies = new List<ExperienceTechnology>
            {
                new() { Name = "React 18" },
                new() { Name = "NestJS" }
            }
        };
        var skillCat = new SkillCategory
        {
            Title = "Backend & APIs",
            SortOrder = 1,
            Skills = new List<SkillItem>
            {
                new() { Name = "Node.js", ProficiencyPercent = 94, IsPrimary = true },
                new() { Name = "NestJS", ProficiencyPercent = 90, IsPrimary = true }
            }
        };

        await context.WorkExperiences.AddAsync(exp);
        await context.SkillCategories.AddAsync(skillCat);
        await context.SaveChangesAsync();

        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var result = await service.GetResumeAsync();

        // Assert
        result.Should().NotBeNull();
        result!.Experiences.Should().HaveCount(1);
        result.Experiences[0].JobTitle.Should().Be("Senior Developer");
        result.Experiences[0].Technologies.Should().Contain("React 18");
        result.SkillCategories.Should().HaveCount(1);
        result.SkillCategories[0].Skills.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetProjectsAsync_FilteredByCategory_ReturnsOnlyMatchingProjects()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var catFullstack = new ProjectCategory { Slug = "fullstack", Label = "Fullstack", IsActive = true };
        var catAi = new ProjectCategory { Slug = "ai-backend", Label = "AI & Backend", IsActive = true };
        await context.ProjectCategories.AddRangeAsync(catFullstack, catAi);

        var p1 = new Project
        {
            Slug = "tslhub",
            Title = "TSL Hub Law Firm",
            IsPublished = true,
            CategoryMaps = new List<ProjectCategoryMap> { new() { ProjectCategory = catFullstack } }
        };

        var p2 = new Project
        {
            Slug = "aitoolkit",
            Title = "AI Productivity Toolkit",
            IsPublished = true,
            CategoryMaps = new List<ProjectCategoryMap> { new() { ProjectCategory = catAi } }
        };

        await context.Projects.AddRangeAsync(p1, p2);
        await context.SaveChangesAsync();

        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var fullstackOnly = await service.GetProjectsAsync("fullstack");
        var allProjects = await service.GetProjectsAsync("all");

        // Assert
        fullstackOnly.Projects.Should().HaveCount(1);
        fullstackOnly.Projects[0].Slug.Should().Be("tslhub");

        allProjects.Projects.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetProjectBySlugAsync_ExistingSlug_ReturnsProjectWithCaseStudy()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var project = new Project
        {
            Slug = "comza",
            Title = "Comza Africa Commercial Ecosystem",
            IsPublished = true,
            HasCaseStudy = true,
            CaseStudy = new CaseStudy
            {
                Title = "Comza Africa",
                Summary = "Detailed case study breakdown",
                Highlights = new List<CaseStudyHighlight> { new() { HighlightText = "Highlight 1" } }
            }
        };

        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();

        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var result = await service.GetProjectBySlugAsync("comza");

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Comza Africa Commercial Ecosystem");
        result.CaseStudy.Should().NotBeNull();
        result.CaseStudy!.Summary.Should().Be("Detailed case study breakdown");
        result.CaseStudy.Highlights.Should().Contain("Highlight 1");
    }

    [Fact]
    public async Task GetProjectBySlugAsync_ValidGuid_ReturnsProject()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var projId = Guid.NewGuid();
        var project = new Project
        {
            Id = projId,
            Slug = "guid-test-project",
            Title = "GUID Test Project",
            IsPublished = true,
            HasCaseStudy = true,
            CaseStudy = new CaseStudy
            {
                Title = "GUID Case Study",
                Summary = "Loaded via GUID string"
            }
        };

        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();

        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var result = await service.GetProjectBySlugAsync(projId.ToString());

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("GUID Test Project");
        result.CaseStudy.Should().NotBeNull();
        result.CaseStudy!.Summary.Should().Be("Loaded via GUID string");
    }

    [Fact]
    public async Task SubmitContactInquiryAsync_ValidData_PersistsInDbAndReturnsSuccess()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var response = await service.SubmitContactInquiryAsync(new CreateInquiryRequestDto
        {
            Name = "John Doe",
            Email = "john@example.com",
            InquiryType = "contract",
            Message = "Need high-impact microservices architecture consulting."
        });

        // Assert
        response.Success.Should().BeTrue();
        response.InquiryId.Should().NotBeNull();

        var inDb = await context.ContactInquiries.FindAsync(response.InquiryId);
        inDb.Should().NotBeNull();
        inDb!.SenderName.Should().Be("John Doe");
        inDb.SenderEmail.Should().Be("john@example.com");
        inDb.InquiryType.Should().Be("contract");
        inDb.IsRead.Should().BeFalse();

        _mockEmail.Verify(e => e.SendContactNotificationAsync("John Doe", "john@example.com", "contract", It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SubmitContactInquiryAsync_EmptyFields_ReturnsErrorWithoutSaving()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var service = new PortfolioPublicService(context, _mockEmail.Object);

        // Act
        var response = await service.SubmitContactInquiryAsync(new CreateInquiryRequestDto
        {
            Name = "",
            Email = "",
            Message = ""
        });

        // Assert
        response.Success.Should().BeFalse();
        response.InquiryId.Should().BeNull();
        context.ContactInquiries.Should().BeEmpty();
    }
}
