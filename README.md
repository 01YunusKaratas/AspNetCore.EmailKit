# 📧 AspNetCore.EmailKit

**AspNetCore.EmailKit**, ASP.NET Core projelerinde hızlı ve güvenli şekilde e-posta göndermek için tasarlanmış, 
basit ama güçlü bir **Email Service Kit**'idir.  
SMTP ayarlarını `appsettings.json` dosyasına tanımlayarak, tek satır kodla e-posta göndermeyi mümkün kılar.

---

## 🚀 Özellikler

- [x] Basit kurulum (tek servis entegrasyonu)
- [x] `IOptions` pattern ile **appsettings.json** desteği  
- [x] Otomatik yapılandırma (`IServiceCollection` extension)
- [x] `ILogger` ile hata ve başarı loglaması
- [x] TLS/SSL destekli güvenli SMTP gönderimi
- [x] Tek satırla mail gönderme (`await _emailService.SendEmailAsync(...)`)

---


## 📦 Kurulum

NuGet üzerinden yükleyebilirsin:

```bash
dotnet add package AspNetCore.EmailKit
```

## Appsettings.json Yapılandırması
```
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderName": "MyApp Mail Service",
  "SenderEmail": "noreply@myapp.com",
  "UserName": "noreply@myapp.com",
  "Password": "uygulama_sifresi"
}

Not: Gmail kullanıyorsan, klasik şifre yerine uygulama şifresi oluşturman gerekir.
Hesap > Güvenlik > “2 Adımlı Doğrulama” aktif > “Uygulama Şifreleri” > Yeni oluştur.
```

## Program.cs / Startup.cs Entegrasyonu
```
using AspNetCore.EmailKit.Extensions;

var builder = WebApplication.CreateBuilder(args);

// EmailKit'i servislere ekle
builder.Services.AddEmailKit(builder.Configuration);

var app = builder.Build();

app.Run();
```

## Kullanım
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
            subject: "E-posta Doğrulama",
            body: "<h2>Hesabınızı doğrulamak için tıklayın</h2>"
        );

        return Ok("Doğrulama maili gönderildi.");
    }
}
```
## Hata Yönetimi & Loglama
[Information] Mail başarıyla gönderildi: test@domain.com / Hoşgeldin
[Error] Mail gönderilirken hata oluştu: Kimlik doğrulama başarısız.

