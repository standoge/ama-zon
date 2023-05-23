using Microsoft.AspNetCore.Mvc;
using Spire.Doc;

namespace ama_zon.Controllers;

[Route("form")]
public class FormController : Controller
{
    // GET
    public IActionResult Index()
    {
        Document doc = new Document();
        doc.LoadFromFile("Controllers/Templates/permanent-template.docx");
        doc.SaveToFile("Controllers/Templates/output.pdf", FileFormat.PDF);
        doc.SaveToFile("wwwroot/files/fromweb.pdf", FileFormat.PDF);
        return View();
    }
}