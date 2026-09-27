using FluentAssertions;
using Moq;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Application.DTOs.Auth;
using Portfolio.Application.Services;
using Portfolio.Domain.Entities;
using Portfolio.Test.Common;
using Xunit;

namespace Portfolio.Test.Auth;

public class AuthServiceTests
{
    private readonly Mock<IPasswordHasher> _mockHasher;
    private readonly Mock<IJwtTokenService> _mockJwt;

    public AuthServiceTests()
    {
        _mockHasher = new Mock<IPasswordHasher>();
        _mockJwt = new Mock<IJwtTokenService>();
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsLoginResponse()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var admin = new AdminUser
        {
            Email = "adejoro@test.com",
            FullName = "Oluwatobi Adejoro",
            PasswordHash = "hashed_pw",
            Role = "Admin"
        };
        await context.AdminUsers.AddAsync(admin);
        await context.SaveChangesAsync();

        _mockHasher.Setup(h => h.VerifyPassword("password123", "hashed_pw")).Returns(true);
        _mockJwt.Setup(j => j.GenerateToken(It.IsAny<AdminUser>())).Returns("jwt_sample_token");

        var service = new AuthService(context, _mockHasher.Object, _mockJwt.Object);

        // Act
        var result = await service.LoginAsync(new LoginRequestDto
        {
            Email = "adejoro@test.com",
            Password = "password123"
        });

        // Assert
        result.Should().NotBeNull();
        result!.Token.Should().Be("jwt_sample_token");
        result.Email.Should().Be("adejoro@test.com");
        result.FullName.Should().Be("Oluwatobi Adejoro");
        result.Role.Should().Be("Admin");
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ReturnsNull()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var admin = new AdminUser
        {
            Email = "adejoro@test.com",
            PasswordHash = "hashed_pw"
        };
        await context.AdminUsers.AddAsync(admin);
        await context.SaveChangesAsync();

        _mockHasher.Setup(h => h.VerifyPassword("wrong_password", "hashed_pw")).Returns(false);

        var service = new AuthService(context, _mockHasher.Object, _mockJwt.Object);

        // Act
        var result = await service.LoginAsync(new LoginRequestDto
        {
            Email = "adejoro@test.com",
            Password = "wrong_password"
        });

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task LoginAsync_NonExistentEmail_ReturnsNull()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var service = new AuthService(context, _mockHasher.Object, _mockJwt.Object);

        // Act
        var result = await service.LoginAsync(new LoginRequestDto
        {
            Email = "nonexistent@test.com",
            Password = "any"
        });

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task ChangePasswordAsync_ValidCurrentPassword_UpdatesHashAndReturnsTrue()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var admin = new AdminUser
        {
            Email = "adejoro@test.com",
            PasswordHash = "old_hash"
        };
        await context.AdminUsers.AddAsync(admin);
        await context.SaveChangesAsync();

        _mockHasher.Setup(h => h.VerifyPassword("old_password", "old_hash")).Returns(true);
        _mockHasher.Setup(h => h.HashPassword("new_password")).Returns("new_hash");

        var service = new AuthService(context, _mockHasher.Object, _mockJwt.Object);

        // Act
        var success = await service.ChangePasswordAsync(admin.Id, new ChangePasswordRequestDto
        {
            CurrentPassword = "old_password",
            NewPassword = "new_password"
        });

        // Assert
        success.Should().BeTrue();
        var updated = await context.AdminUsers.FindAsync(admin.Id);
        updated!.PasswordHash.Should().Be("new_hash");
    }

    [Fact]
    public async Task ChangePasswordAsync_IncorrectCurrentPassword_ReturnsFalse()
    {
        // Arrange
        using var context = TestDbContextFactory.Create();
        var admin = new AdminUser
        {
            Email = "adejoro@test.com",
            PasswordHash = "old_hash"
        };
        await context.AdminUsers.AddAsync(admin);
        await context.SaveChangesAsync();

        _mockHasher.Setup(h => h.VerifyPassword("wrong_old", "old_hash")).Returns(false);

        var service = new AuthService(context, _mockHasher.Object, _mockJwt.Object);

        // Act
        var success = await service.ChangePasswordAsync(admin.Id, new ChangePasswordRequestDto
        {
            CurrentPassword = "wrong_old",
            NewPassword = "new_password"
        });

        // Assert
        success.Should().BeFalse();
        var updated = await context.AdminUsers.FindAsync(admin.Id);
        updated!.PasswordHash.Should().Be("old_hash");
    }
}
