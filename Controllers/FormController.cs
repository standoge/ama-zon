using Microsoft.AspNetCore.Mvc;
using Spire.Doc;

namespace ama_zon.Controllers;

public class FormController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}