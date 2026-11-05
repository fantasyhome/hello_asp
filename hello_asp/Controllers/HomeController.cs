using Microsoft.AspNetCore.Mvc;

namespace hello_asp.Controllers;

public class HomeController
{
    [Route("HomeController")]
    public string Controller1()
    {
        return "Hello Controller!";
    }
}