using Microsoft.AspNetCore.Mvc;

namespace Transport.Web.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}