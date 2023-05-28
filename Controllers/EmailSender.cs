using System.Net;
using System.Net.Mail;

namespace ama_zon.Controllers;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string receiver, string subject, string body, MemoryStream file)
    {
        string sender = "";
        string password = "";

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(sender, password)
        };

        MailMessage message = new MailMessage();
        Attachment document = new Attachment(file, "NDA.pdf");
        message.From = new MailAddress(sender);
        message.To.Add(receiver);
        message.Subject = subject;
        message.Body = body;
        message.Attachments.Add(document);

        return client.SendMailAsync(message);
    }
}