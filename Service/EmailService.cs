using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using AspNetCore.EmailKit.Models;
using AspNetCore.EmailKit.Interface;


namespace AspNetCore.EmailKit.Service;

public class EmailService : IEmailService
{   

    private readonly ILogger<EmailService> _logger;
    private readonly EmailSettings _settings;

    public EmailService(ILogger<EmailService> logger, IOptions<EmailSettings> options)
    {
        _logger = logger;
        _settings = options.Value;
    }
 
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = body };
        message.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.UserName, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Mail başarıyla gönderildi: {toEmail} / {subject}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Mail gönderilirken hata oluştu,lütfen daha sonra tekrar deneyiniz: {ex.Message}");
            throw;
        }
    }
}
