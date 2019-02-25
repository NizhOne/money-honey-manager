using API.Constants;
using API.Interfaces;
using API.Models.Domain;
using API.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfirmationOptions emailConfirmation;
        private readonly AuthenticationOptions authentication;

        public EmailService(IOptions<EmailConfirmationOptions> emailOptionsAccessor, IOptions<AuthenticationOptions> authOptionsAccessor)
        {
            emailConfirmation = emailOptionsAccessor.Value;
            authentication = authOptionsAccessor.Value;
        }

        public async Task SendConfirmationMail(ApplicationUser user, string code)
        {
            var callbackUrl = String.Format(EmailConfirmationConstants.Endpoint, authentication.BackendHost, user.Id, code);

            await this.SendEmailAsync(EmailConfirmationConstants.EmailSubject,
                          string.Format(EmailConfirmationConstants.EmailBody, HtmlEncoder.Default.Encode(callbackUrl)),
                user.Email);
        }

        private async Task SendEmailAsync(string subject, string message, string email)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(this.emailConfirmation.EmailAddress);
            mail.To.Add(new MailAddress(email));
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(this.emailConfirmation.SmtpServer, this.emailConfirmation.SmtpPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(this.emailConfirmation.EmailAddress, this.emailConfirmation.EmailPassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}

