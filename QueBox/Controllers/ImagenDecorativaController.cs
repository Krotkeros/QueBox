using Microsoft.AspNetCore.Mvc;

namespace QueBox.Controllers;

public class ImagenDecorativaController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}