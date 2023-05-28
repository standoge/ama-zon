using System.Net;
using System.Net.Mail;
namespace ama_zon.Controllers;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string receiver, string subject, string message)
    {
        string sender = "";
        string password = "";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(sender, password)
        };

        return client.SendMailAsync(
            new MailMessage(
                from: sender,
                to: receiver,
                subject,
                message));
    }
}