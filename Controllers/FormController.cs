using Microsoft.AspNetCore.Mvc;
using Spire.Doc;

namespace ama_zon.Controllers;

[Route("form")]
public class FormController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}