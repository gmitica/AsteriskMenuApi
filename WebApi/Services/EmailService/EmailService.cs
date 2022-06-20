using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Helpers;

namespace WebApi.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;


        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public void SendEmailAsync(string email, string subject, string messageBody)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_appSettings.EmailUser, _appSettings.EmailFromDisplayName);
                message.To.Add(new MailAddress(email));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = messageBody;
                smtp.Port = _appSettings.EmailPort;
                smtp.Host = _appSettings.EmailHost;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_appSettings.EmailUser, _appSettings.EmailPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {
               Console.WriteLine(e.ToString());
            }
        }
    }
}