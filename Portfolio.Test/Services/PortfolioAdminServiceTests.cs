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
}
