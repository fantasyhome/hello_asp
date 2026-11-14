using Microsoft.AspNetCore.Mvc;

namespace hello_asp.Controllers;

public class FirstViewController : Controller
{
    [Route("FirstView")]
    public IActionResult FirstView()
    {
        return View(); // Views/FirstView/FirstView.cshtml
    }
}