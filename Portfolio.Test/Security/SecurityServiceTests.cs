using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Services;
using Xunit;

namespace Portfolio.Test.Security;

public class SecurityServiceTests
{
    [Fact]
    public void PasswordHasher_HashesAndVerifiesPasswordCorrectly()
    {
        // Arrange
        var hasher = new PasswordHasher();
        var rawPassword = "SecurePassword123!";

        // Act
        var hash = hasher.HashPassword(rawPassword);
        var isValid = hasher.VerifyPassword(rawPassword, hash);
        var isInvalid = hasher.VerifyPassword("WrongPassword123!", hash);

        // Assert
        hash.Should().NotBeNullOrWhiteSpace();
        hash.Should().NotBe(rawPassword);
        isValid.Should().BeTrue();
        isInvalid.Should().BeFalse();
    }

    [Fact]
    public void JwtTokenService_GeneratesTokenWithAdminClaims()
    {
        // Arrange
        var configValues = new Dictionary<string, string?>
        {
            { "Jwt:Secret", "SuperSecretKeyForTestingJwtTokensOnly32BytesLong!" },
            { "Jwt:Issuer", "PortfolioApiTest" },
            { "Jwt:Audience", "PortfolioAdminTest" },
            { "Jwt:ExpirationInDays", "7" }
        };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(configValues).Build();
        var jwtService = new JwtTokenService(configuration);

        var user = new AdminUser
        {
            Id = Guid.NewGuid(),
            Email = "Adejorotgold1@yahoo.com",
            FullName = "Oluwatobi Adejoro",
            Role = "Admin"
        };

        // Act
        var token = jwtService.GenerateToken(user);

        // Assert
        token.Should().NotBeNullOrWhiteSpace();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        jwt.Issuer.Should().Be("PortfolioApiTest");
        jwt.Audiences.Should().Contain("PortfolioAdminTest");
        jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value.Should().Be(user.Id.ToString());
        jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value.Should().Be("Adejorotgold1@yahoo.com");
        jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value.Should().Be("Admin");
    }
}
