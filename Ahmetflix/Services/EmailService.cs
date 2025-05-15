using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Ahmetflix.Services
{
    public class EmailService : IEmailSender
    {
        private readonly ILogger<EmailService> _logger;
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _apiKey = configuration["SendGrid:ApiKey"] ?? throw new InvalidOperationException("SendGrid API key not found.");
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("noreply@ahmetflix.com", "Ahmetflix");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlMessage);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                _logger.LogError("Failed to send email to {Email}. Status code: {StatusCode}", email, response.StatusCode);
            }
        }
    }
} 