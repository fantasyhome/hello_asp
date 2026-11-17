using Microsoft.AspNetCore.Mvc;

namespace hello_asp.Controllers;

public class ProductsController : Controller
{
    
    [Route("Products")]
    public IActionResult ProductsWithLayout()
    {
        return View();
    }
    
    [Route("Customer")]
    public IActionResult Customer()
    {
        return View();
    }
}