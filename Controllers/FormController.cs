using Microsoft.AspNetCore.Mvc;
using Spire.Doc;

namespace ama_zon.Controllers;

[Route("form")]
public class FormController : Controller
{
    // GET
    public IActionResult Index()
    {
        Document doc = new ();
        doc.LoadFromFile("Controllers/Templates/permanent-template.docx");
        doc.SaveToFile("wwwroot/files/output.pdf", FileFormat.PDF);
        return View();
    }


    [HttpGet("form/download")]
    public async Task<FileContentResult> DownloadPdf()
    {
        const string path = "wwwroot/files/output.pdf";
        var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
        System.IO.File.Delete(path);
        return File(fileBytes, "application/pdf", "output.pdf");
    }
}