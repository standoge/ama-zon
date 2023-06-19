using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Spire.Doc;
using System.Diagnostics.Contracts;
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
        return View();
    }


    // funcion que ejecuta la la logica para descargar el PDF
    [HttpGet("/download")]
    public async Task<FileContentResult> DownloadPdf()
    {
        const string path = "wwwroot/files/output.pdf";
        var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
        System.IO.File.Delete(path);
        return File(fileBytes, "application/pdf", "output.pdf");
    }

    // funcion que ejecuta la la logica para enviar el correo
    [Route("/send")]
    public async Task<IActionResult> SendForm()
    {
        var message = new MimeMessage();

        // obtencion de credenciales desde el sistema de secretos de .NET
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
        message.To.Add(new MailboxAddress("receiver", "sya94572@zbock.com"));
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

    [HttpPost]
    public ActionResult Index(string Nombre, string Apellidos, string Direccion, string pais, string departamento)
    {
        DateTime Fecha;
        string nombre = Nombre + " " + Apellidos;
        Fecha = DateTime.Today;
        Document document = new Document("Controllers/Templates/permanent-template.docx");
        Document newdoc = document.Clone();
        newdoc.SaveToFile("Controllers/Templates/output.docx");
        newdoc.LoadFromFile("Controllers/Templates/output.docx");
        newdoc.Replace("$nombre_receptor", nombre, false, true);
        newdoc.Replace("$direccion_receptor", Direccion, false, true);
        newdoc.Replace("$fecha_emision", Fecha.ToString("D"), false, true);
        newdoc.Replace("$pais", pais, false, true);
        newdoc.Replace("$departamento", departamento, false, true);
        newdoc.SaveToFile("Controllers/Templates/output.docx");

        newdoc.LoadFromFile("Controllers/Templates/output.docx");
        newdoc.SaveToFile("wwwroot/files/output.pdf", FileFormat.PDF);

        newdoc.Close();
        return View();
    }
}