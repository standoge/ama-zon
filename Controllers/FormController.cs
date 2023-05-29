using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Spire.Doc;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ama_zon.Controllers;

[Route("form")]
public class FormController : Controller
{
    private readonly IConfiguration _configuration;

    public FormController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET
    public IActionResult Index()
    {
        Document doc = new();
        doc.LoadFromFile("Controllers/Templates/permanent-template.docx");
        doc.SaveToFile("wwwroot/files/output.pdf", FileFormat.PDF);

        return View();
    }


    [HttpGet("/download")]
    public async Task<FileContentResult> DownloadPdf()
    {
        const string path = "wwwroot/files/output.pdf";
        var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
        System.IO.File.Delete(path);
        return File(fileBytes, "application/pdf", "output.pdf");
    }

    [Route("/send")]
    public async Task<IActionResult> SendForm()
    {
        var message = new MimeMessage();

        string email = _configuration["FormController:email"];
        string password = _configuration["FormController:password"];

        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(System.IO.File.OpenRead("wwwroot/files/output.pdf")),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = "output.pdf"
        };

        message.From.Add(new MailboxAddress("sender", email));
        message.To.Add(new MailboxAddress("receiver", ""));
        message.Subject = "Acuerdo de confidencialidad laboral";
        message.Body = new Multipart("mixed")
        {
            attachment
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(email, password);

            client.Send(message);
            client.Disconnect(true);
        }

        return View(nameof(Index));
    }
}