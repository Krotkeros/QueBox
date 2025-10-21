using Microsoft.AspNetCore.Mvc;

namespace QueBox.Controllers;

public class UsuarioController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}