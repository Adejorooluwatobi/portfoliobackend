using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Portfolio.Application.Common.Interfaces;

namespace Portfolio.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendContactNotificationAsync(string senderName, string senderEmail, string inquiryType, string message, CancellationToken cancellationToken = default)
    {
        var smtpHost = _configuration["Email:SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = int.TryParse(_configuration["Email:SmtpPort"], out var port) ? port : 587;
        var smtpUser = _configuration["Email:SmtpUser"] ?? "";
        var smtpPass = _configuration["Email:SmtpPassword"] ?? "";
        var recipientEmail = _configuration["Email:RecipientEmail"] ?? "Adejorotgold1@yahoo.com";
        var fromEmail = _configuration["Email:FromEmail"] ?? smtpUser;

        // If credentials are not yet configured, log notification info and return safely
        if (string.IsNullOrWhiteSpace(smtpUser) || string.IsNullOrWhiteSpace(smtpPass))
        {
            _logger.LogInformation("SMTP credentials not configured. Contact inquiry logged: From '{Name}' <{Email}> for '{Type}': {Message}",
                senderName, senderEmail, inquiryType, message);
            return true;
        }

        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Portfolio Contact Form", fromEmail));
            emailMessage.To.Add(new MailboxAddress("Oluwatobi Adejoro", recipientEmail));
            emailMessage.ReplyTo.Add(new MailboxAddress(senderName, senderEmail));
            emailMessage.Subject = $"[Portfolio Inquiry] New {inquiryType.ToUpper()} message from {senderName}";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
<!DOCTYPE html>
<html>
<head>
  <style>
    body {{ font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background-color: #0c1322; color: #f8fafc; padding: 20px; }}
    .container {{ max-width: 600px; margin: 0 auto; background-color: #111a2e; border: 1px solid #1e293b; border-radius: 12px; padding: 28px; }}
    .badge {{ display: inline-block; background-color: #06b6d4; color: #020617; font-weight: 700; font-size: 12px; padding: 4px 10px; border-radius: 9999px; text-transform: uppercase; margin-bottom: 12px; }}
    h2 {{ margin-top: 0; color: #f8fafc; font-size: 20px; }}
    .field {{ margin-bottom: 16px; }}
    .label {{ font-size: 11px; text-transform: uppercase; color: #94a3b8; font-weight: 600; letter-spacing: 0.05em; margin-bottom: 4px; }}
    .value {{ font-size: 15px; color: #f8fafc; }}
    .message-box {{ background-color: #080e1a; border-left: 3px solid #06b6d4; padding: 16px; border-radius: 6px; font-size: 14px; line-height: 1.6; white-space: pre-wrap; }}
    .footer {{ margin-top: 24px; padding-top: 16px; border-top: 1px solid #1e293b; font-size: 12px; color: #64748b; }}
  </style>
</head>
<body>
  <div class='container'>
    <div class='badge'>New Inquiry Notification</div>
    <h2>Someone reached out through your Portfolio website</h2>
    <div class='field'>
      <div class='label'>Sender Name</div>
      <div class='value'><strong>{senderName}</strong></div>
    </div>
    <div class='field'>
      <div class='label'>Sender Email</div>
      <div class='value'><a href='mailto:{senderEmail}' style='color: #06b6d4;'>{senderEmail}</a></div>
    </div>
    <div class='field'>
      <div class='label'>Inquiry Scope</div>
      <div class='value'>{inquiryType}</div>
    </div>
    <div class='field'>
      <div class='label'>Message</div>
      <div class='message-box'>{message}</div>
    </div>
    <div class='footer'>
      Received at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC • Reply directly to this email to contact the sender.
    </div>
  </div>
</body>
</html>",
                TextBody = $"New Inquiry from {senderName} ({senderEmail})\nScope: {inquiryType}\n\nMessage:\n{message}\n\nReceived at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC"
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(smtpUser, smtpPass, cancellationToken);
            await client.SendAsync(emailMessage, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation("Contact notification email sent successfully to {Recipient}", recipientEmail);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send contact notification email via SMTP.");
            return false;
        }
    }
}
