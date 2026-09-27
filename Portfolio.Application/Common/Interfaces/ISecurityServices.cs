using Portfolio.Domain.Entities;

namespace Portfolio.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(AdminUser user);
}

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
