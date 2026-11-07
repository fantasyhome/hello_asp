using Microsoft.AspNetCore.Mvc;
using hello_asp.Models;
namespace hello_asp.Controllers;

public class HomeController : Controller
{
    [Route("HomeController")]
    public ContentResult Controller1()
    {
        return Content("Hello Controller!", "text/plain");
    }

    [Route("person")]
    public JsonResult Person()
    {
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
        };
        return Json(person); // return new JsonResult(person);
    }
}