# 📧 AspNetCore.EmailKit

**AspNetCore.EmailKit** is a lightweight yet powerful email service kit designed for ASP.NET Core applications.  
It allows you to send emails securely and effortlessly by defining SMTP settings in `appsettings.json` and calling a single line of code.

---

## 🚀 Features

- [x] Simple setup (single service integration)
- [x] `IOptions` pattern support with **appsettings.json**
- [x] Automatic configuration via `IServiceCollection` extension
- [x] Built-in `ILogger` for success and error logging
- [x] Secure SMTP with TLS/SSL support
- [x] Send email in one line — `await _emailService.SendEmailAsync(...)`

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package AspNetCore.EmailKit
```

## Appsettings.json Configuration
```
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderName": "MyApp Mail Service",
  "SenderEmail": "noreply@myapp.com",
  "UserName": "noreply@myapp.com",
  "Password": "app_specific_password"
}

Note: If you’re using Gmail, you must enable 2-Step Verification and create an App Password.
Go to: Google Account → Security → App Passwords → Create New.
```

## Integration in Program.cs / Startup.cs
```
using AspNetCore.EmailKit.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register EmailKit
builder.Services.AddEmailKit(builder.Configuration);

var app = builder.Build();

app.Run();

```

## Usage Example
```
using AspNetCore.EmailKit.Interface;

public class AccountController : ControllerBase
{
    private readonly IEmailService _emailService;

    public AccountController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send-confirmation")]
    public async Task<IActionResult> SendConfirmationMail(string email)
    {
        await _emailService.SendEmailAsync(
            toEmail: email,
            subject: "Email Confirmation",
            body: "<h2>Click below to confirm your account</h2>"
        );

        return Ok("Confirmation email sent successfully.");
    }
}

```
## Error Handling & Logging
[Information] Email sent successfully: test@domain.com / Welcome
[Error] Error while sending email: Authentication failed.

