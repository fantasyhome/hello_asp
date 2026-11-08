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
    public FileContentResult FileDownload3()
    {
        byte[] bytes = System.IO.File.ReadAllBytes("wwwroot/02_3.jpg");
        return new FileContentResult(bytes, "image/jpeg");
    }
}