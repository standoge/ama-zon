namespace ama_zon.Controllers;

public interface IEmailSender
{
    Task SendEmailAsync(string receiver, string subject, string body, MemoryStream file);
}