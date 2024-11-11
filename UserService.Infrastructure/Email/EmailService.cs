using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace UserService.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                string host = _configuration["SmtpSettings:Host"];
                int port = int.Parse(_configuration["SmtpSettings:Port"]);
                string username = _configuration["SmtpSettings:Username"];
                string password = _configuration["SmtpSettings:Password"];
                string from = _configuration["SmtpSettings:From"];
                bool enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(from))
                {
                    _logger.LogError("Email configuration is invalid. Please check your settings.");
                    return false;
                }

                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enableSsl,
                    Credentials = new NetworkCredential(username, password),
                    Timeout = 10000
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully to {Recipient}", to);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error occurred while sending email to {Recipient}", to);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while sending email to {Recipient}", to);
                return false;
            }
        }
    }
}
