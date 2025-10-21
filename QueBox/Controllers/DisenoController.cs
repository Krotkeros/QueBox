using Microsoft.AspNetCore.Mvc;

namespace QueBox.Controllers;

public class DisenoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}