namespace Portfolio.Application.Common.Interfaces;

public interface IEmailService
{
    Task<bool> SendContactNotificationAsync(string senderName, string senderEmail, string inquiryType, string message, CancellationToken cancellationToken = default);
}
