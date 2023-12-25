using Microsoft.AspNetCore.Mvc;


namespace Simples_Data.Controllers;
public class MainController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public MainController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

}