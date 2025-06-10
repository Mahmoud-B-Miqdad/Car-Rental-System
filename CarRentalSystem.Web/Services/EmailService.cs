using CarRentalSystem.Web.Interfaces;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarRentalSystem.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var senderEmail = _configuration["SendGrid:FromEmail"];
            var senderName = _configuration["SendGrid:FromName"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, senderName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}