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

    [Route("file-download")]
    public VirtualFileResult FileDownload()
    {
        Response.Headers.ContentDisposition = "inline"; //设置浏览器直接显示
        return new VirtualFileResult("~/02_3.jpg", "image/jpeg");
    }
    
    [Route("file-download2")]
    public PhysicalFileResult FileDownload2()
    {
        return new PhysicalFileResult(@"c:\work\hello_asp\hello_asp\wwwroot\02_3.jpg", "image/jpeg")
        {
            FileDownloadName = "my-picture.jpg" // 设置文件名且直接下载
        };
    }
    
    [Route("file-download3")]
    public IActionResult FileDownload3() // IActionResult is the parent of all result
    {
        byte[] bytes = System.IO.File.ReadAllBytes("wwwroot/02_3.jpg");
        return new FileContentResult(bytes, "image/jpeg");
    }

    [Route("Book")]
    public IActionResult OldBook()
    {
        return new LocalRedirectResult("/NewBook",true); // permanent == true浏览器可能记住旧地址到新地址的映射
    }
    
    // Route Parameter has higher priority than query string while model binding
    // The priority of model binding is: form fields, request body, route data, query string parameters
    // http://localhost:5120/NewBook/1/?bookId=-10&IsLoggedIn=true bookId will be 1,
    // but you can specify the source of model binding
    [Route("NewBook/{bookId?}")]
    public IActionResult Book([FromQuery]int? bookId)
    {
        // if (!Request.Query.ContainsKey("BookId"))
        // {
        //     return BadRequest("BookId is missing");
        // }
        if (!bookId.HasValue)
        {
            return BadRequest("BookId is missing");
        }
        
        if (bookId <= 0 || bookId > 1000)
        {
            Response.StatusCode = 404;
            return Content("BookId is invalid");
        }

        if (Convert.ToBoolean(Request.Query["IsLoggedIn"]) == false)
        {
            Response.StatusCode = 401;
            return Unauthorized("User unauthorized");
        }
        
        return File("/02_3.jpg","image/jpeg");
    }

    [Route("register")]
    public IActionResult Register(Person person)
    {
        
        if (!ModelState.IsValid)
        {
            List<string> errorList = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }
            
            string errorMessage = string.Join("\n", errorList);
            return BadRequest(errorMessage);
        }

        return Content($"{person}");
    }
}