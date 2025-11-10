using AspNetCore.EmailKit.Interface;
using AspNetCore.EmailKit.Models;
using AspNetCore.EmailKit.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.EmailKit.Extensions
{
    public static class AspNetCoreExtensionsKit
    {
        public static IServiceCollection AddEmailKit(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
