using CarRentalSystem.Web.Interfaces;
using CarRentalSystem.Web.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarRentalSystem.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridSettings _settings;

        public EmailService(IOptions<SendGridSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var client = new SendGridClient(_settings.ApiKey);
            var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}