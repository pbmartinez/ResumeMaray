using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace AzureFunctionApp.Services
{
    public interface IEmailService
    {
        Task<Response> SendEmailAsync(string interestedPersonEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;
        private readonly string _emailTo;

        public EmailService(string sendGridApiKey, string emailTo)
        {
            _sendGridApiKey = sendGridApiKey;
            _emailTo = emailTo;
        }

        public async Task<Response> SendEmailAsync(string interestedPersonEmail, string subject, string body)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(_emailTo, "Desde la app web");
            var to = new EmailAddress(_emailTo);
            var plainTextContent = $"Solicitud de {interestedPersonEmail}: \n{body}";
            var htmlContent = $"<strong>Hola !</strong> - Solicitud de {interestedPersonEmail}: \n{body}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            return await client.SendEmailAsync(msg);
        }
    }
}
