using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Data;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace HousingHubBackend.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly HousingHubDBContext _context;
        private readonly IConfiguration _configuration;
        public NotificationService(HousingHubDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void SendNotification(string to, string subject, string message)
        {
            SendEmailAsync(to, subject, message).Wait();
        }

        private async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSection = _configuration.GetSection("Smtp");
            var host = smtpSection["Host"];
            var portString = smtpSection["Port"];
            if (string.IsNullOrWhiteSpace(portString))
                throw new InvalidOperationException("SMTP port configuration is missing.");
            var port = int.Parse(portString);
            var username = smtpSection["Username"];
            var password = smtpSection["Password"];
            var from = smtpSection["From"] ?? username;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}