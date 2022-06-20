using System.Threading.Tasks;

namespace WebApi.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmailAsync(string email, string subject, string messageBody);
    }
}