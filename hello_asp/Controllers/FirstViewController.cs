using Microsoft.AspNetCore.Mvc;

namespace hello_asp.Controllers;

public class FirstViewController : Controller
{
    [Route("FirstView")]
    public IActionResult FirstView()
    {
        return View(); // Views/FirstView/FirstView.cshtml
    }

    [Route("ViewData")]
    public IActionResult Index()
    {
        ViewData["Data"] = "Data from Controller";
        return View();
        // 先搜索Views/FirstView/Index.html,再搜索Views/Shared/Index.html
    }

    [Route("Products")]
    public IActionResult ProductsWithLayout()
    {
        return View();
    }
}