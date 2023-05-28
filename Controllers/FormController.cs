using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Spire.Doc;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ama_zon.Controllers;

[Route("form")]
public class FormController : Controller
{
    // GET
    public IActionResult Index()
    {
        Document doc = new();
        doc.LoadFromFile("Controllers/Templates/permanent-template.docx");
        doc.SaveToFile("wwwroot/files/output.pdf", FileFormat.PDF);
        
        
        var message = new MimeMessage();
        
        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(System.IO.File.OpenRead("wwwroot/files/output.pdf")),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = "output.pdf"
        };
        
        message.From.Add(new MailboxAddress("sender", ""));
        message.To.Add(new MailboxAddress("receiver", ""));
        message.Subject = "NDA";
        message.Body = new TextPart(
            "plain")
        {
            Text = "Gone away"
        };
        
         message.Body = new Multipart("mixed")
         {
             attachment
         };
        
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate("", "");

            client.Send(message);
            client.Disconnect(true);
        }

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

    [HttpPost]
    public IActionResult SendForm()
    {
        return Ok();
    }
}