using FluentAssertions;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;
using Portfolio.Domain.Entities;
using Portfolio.Test.Common;
using Xunit;

namespace Portfolio.Test.Services;

public class PortfolioAdminServiceTests
{
    [Fact]
    public async Task UpdateProfileAsync_UpdatesFieldsSuccessfully()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var profile = new Profile { FullName = "Old Name", Email = "old@mail.com" };
        await context.Profiles.AddAsync(profile);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act
        var success = await service.UpdateProfileAsync(new UpdateProfileRequestDto
        {
            FullName = "Oluwatobi Adejoro",
            PrimaryTitle = "Senior Fullstack Engineer",
            Email = "Adejorotgold1@yahoo.com",
            YearsExperience = 5,
            ProjectsCompleted = 30,
            IsAvailable = true
        });

        // Assert
        success.Should().BeTrue();
        var updated = await context.Profiles.FindAsync(profile.Id);
        updated!.FullName.Should().Be("Oluwatobi Adejoro");
        updated.Email.Should().Be("Adejorotgold1@yahoo.com");
        updated.YearsExperience.Should().Be(5);
    }

    [Fact]
    public async Task CreateProjectAsync_AddsProjectWithTagsAndCategories()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var category = new ProjectCategory { Slug = "fullstack", Label = "Fullstack" };
        await context.ProjectCategories.AddAsync(category);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act
        var created = await service.CreateProjectAsync(new ProjectCreateUpdateDto
        {
            Slug = "new-saas-platform",
            Title = "New SaaS Platform",
            ClientName = "Enterprise Client",
            ShortDescription = "High throughput cloud SaaS platform",
            IsPublished = true,
            CategoryIds = new List<Guid> { category.Id },
            Tags = new List<string> { "React", "ASP.NET Core", "PostgreSQL" }
        });

        // Assert
        created.Should().NotBeNull();
        created.Slug.Should().Be("new-saas-platform");

        var inDb = await service.GetProjectByIdAsync(created.Id);
        inDb.Should().NotBeNull();
        inDb!.Tags.Should().HaveCount(3);
        inDb.Tags.Select(t => t.TagName).Should().Contain("React");
        inDb.CategoryMaps.Should().HaveCount(1);
    }

    [Fact]
    public async Task UpdateCaseStudyAsync_CreatesOrUpdatesCaseStudyForProject()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var project = new Project { Slug = "client-app", Title = "Client App" };
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act
        var success = await service.UpdateCaseStudyAsync(project.Id, new CaseStudyUpdateDto
        {
            Title = "Client App Case Study",
            CategoryLabel = "Fullstack Enterprise",
            Summary = "Architecture case study overview",
            Highlights = new List<string> { "38% bundle reduction", "Sub-second response" },
            Technologies = new List<string> { "NestJS", "TypeScript" }
        });

        // Assert
        success.Should().BeTrue();
        var cs = await service.GetCaseStudyAsync(project.Id);
        cs.Should().NotBeNull();
        cs!.Title.Should().Be("Client App Case Study");
        cs.Highlights.Should().HaveCount(2);
        cs.Technologies.Should().HaveCount(2);
    }

    [Fact]
    public async Task ToggleInquiryReadAsync_FlipsReadStatusAndSetsTimestamp()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var inquiry = new ContactInquiry
        {
            SenderName = "Recruiter",
            SenderEmail = "recruiter@hiring.com",
            Message = "Looking to hire senior dev",
            IsRead = false
        };
        await context.ContactInquiries.AddAsync(inquiry);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act 1: Mark as read
        var readSuccess = await service.ToggleInquiryReadAsync(inquiry.Id);

        // Assert 1
        readSuccess.Should().BeTrue();
        var updated = await context.ContactInquiries.FindAsync(inquiry.Id);
        updated!.IsRead.Should().BeTrue();
        updated.ReadAt.Should().NotBeNull();

        // Act 2: Toggle back to unread
        await service.ToggleInquiryReadAsync(inquiry.Id);
        var unread = await context.ContactInquiries.FindAsync(inquiry.Id);
        unread!.IsRead.Should().BeFalse();
        unread.ReadAt.Should().BeNull();
    }

    [Fact]
    public async Task GetInquiryStatsAsync_CalculatesCorrectMetrics()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var i1 = new ContactInquiry { SenderName = "A", SenderEmail = "a@test.com", Message = "m1", IsRead = false, IsArchived = false, SubmittedAt = DateTime.UtcNow };
        var i2 = new ContactInquiry { SenderName = "B", SenderEmail = "b@test.com", Message = "m2", IsRead = true, IsArchived = false, SubmittedAt = DateTime.UtcNow };
        var i3 = new ContactInquiry { SenderName = "C", SenderEmail = "c@test.com", Message = "m3", IsRead = true, IsArchived = true, SubmittedAt = DateTime.UtcNow.AddDays(-5) };

        await context.ContactInquiries.AddRangeAsync(i1, i2, i3);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act
        var stats = await service.GetInquiryStatsAsync();

        // Assert
        stats.TotalCount.Should().Be(3);
        stats.UnreadCount.Should().Be(1);
        stats.ArchivedCount.Should().Be(1);
        stats.TodayCount.Should().Be(2);
    }

    [Fact]
    public async Task CreateProjectAsync_WhenCreatedWithDefaultSortOrder_ShiftsExistingProjectsDown()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var p1 = new Project { Slug = "proj-1", Title = "P1", SortOrder = 1 };
        var p2 = new Project { Slug = "proj-2", Title = "P2", SortOrder = 2 };
        await context.Projects.AddRangeAsync(p1, p2);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act: Create new project with SortOrder 1 (default top)
        var created = await service.CreateProjectAsync(new ProjectCreateUpdateDto
        {
            Slug = "proj-new",
            Title = "New Project",
            SortOrder = 1
        });

        // Assert
        created.SortOrder.Should().Be(1);
        var refreshedP1 = await context.Projects.FindAsync(p1.Id);
        var refreshedP2 = await context.Projects.FindAsync(p2.Id);
        refreshedP1!.SortOrder.Should().Be(2);
        refreshedP2!.SortOrder.Should().Be(3);

        // All projects sorted
        var list = await service.GetProjectsAsync();
        list.Select(p => p.Slug).Should().ContainInOrder("proj-new", "proj-1", "proj-2");
    }

    [Fact]
    public async Task CreateArticleAsync_WhenCreatedWithDefaultSortOrder_ShiftsExistingArticlesDown()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var a1 = new Article { Slug = "art-1", Title = "Article 1", SortOrder = 1 };
        var a2 = new Article { Slug = "art-2", Title = "Article 2", SortOrder = 2 };
        await context.Articles.AddRangeAsync(a1, a2);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act: Create new article at top
        var created = await service.CreateArticleAsync(new ArticleCreateUpdateDto
        {
            Slug = "art-new",
            Title = "New Article",
            SortOrder = 1
        });

        // Assert
        created.SortOrder.Should().Be(1);
        var refreshedA1 = await context.Articles.FindAsync(a1.Id);
        var refreshedA2 = await context.Articles.FindAsync(a2.Id);
        refreshedA1!.SortOrder.Should().Be(2);
        refreshedA2!.SortOrder.Should().Be(3);

        var list = await service.GetArticlesAsync();
        list.Select(a => a.Slug).Should().ContainInOrder("art-new", "art-1", "art-2");
    }

    [Fact]
    public async Task CreateExperienceAsync_WhenCreatedWithDefaultSortOrder_ShiftsExistingExperiencesDown()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var w1 = new WorkExperience { JobTitle = "Role 1", CompanyName = "Co 1", SortOrder = 1 };
        var w2 = new WorkExperience { JobTitle = "Role 2", CompanyName = "Co 2", SortOrder = 2 };
        await context.WorkExperiences.AddRangeAsync(w1, w2);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act: Create new experience at top
        var created = await service.CreateExperienceAsync(new WorkExperienceCreateUpdateDto
        {
            JobTitle = "Lead Architect",
            CompanyName = "Enterprise Inc",
            SortOrder = 1
        });

        // Assert
        created.SortOrder.Should().Be(1);
        var refreshedW1 = await context.WorkExperiences.FindAsync(w1.Id);
        var refreshedW2 = await context.WorkExperiences.FindAsync(w2.Id);
        refreshedW1!.SortOrder.Should().Be(2);
        refreshedW2!.SortOrder.Should().Be(3);

        var list = await service.GetExperiencesAsync();
        list.Select(w => w.JobTitle).Should().ContainInOrder("Lead Architect", "Role 1", "Role 2");
    }

    [Fact]
    public async Task UpdateProjectAsync_WhenTargetSortOrderOccupied_SwapsWithOccupant()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var p1 = new Project { Slug = "p1", Title = "P1", SortOrder = 1 };
        var p2 = new Project { Slug = "p2", Title = "P2", SortOrder = 2 };
        var p3 = new Project { Slug = "p3", Title = "P3", SortOrder = 3 };
        await context.Projects.AddRangeAsync(p1, p2, p3);
        await context.SaveChangesAsync();

        var service = new PortfolioAdminService(context);

        // Act: Move p3 from position 3 to position 1
        var success = await service.UpdateProjectAsync(p3.Id, new ProjectCreateUpdateDto
        {
            Slug = "p3",
            Title = "P3 Updated",
            SortOrder = 1
        });

        // Assert
        success.Should().BeTrue();
        var refreshedP3 = await context.Projects.FindAsync(p3.Id);
        var refreshedP1 = await context.Projects.FindAsync(p1.Id);
        var refreshedP2 = await context.Projects.FindAsync(p2.Id);

        refreshedP3!.SortOrder.Should().Be(1);
        refreshedP1!.SortOrder.Should().Be(3); // Swapped!
        refreshedP2!.SortOrder.Should().Be(2); // Untouched!

        var list = await service.GetProjectsAsync();
        list.Select(p => p.Slug).Should().ContainInOrder("p3", "p2", "p1");
    }
}
