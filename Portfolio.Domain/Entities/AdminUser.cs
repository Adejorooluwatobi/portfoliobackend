using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class AdminUser : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "Admin";
    public DateTime? LastLoginAt { get; set; }
}
