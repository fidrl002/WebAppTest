using Microsoft.AspNetCore.Mvc;

namespace WebAppTest.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RazorTest(int? id, string? value, string text)
        {
            // id url route segment
            ViewBag.id = id;

            // query string value
            ViewBag.qsValue = value;

            //form value
            ViewBag.formValue = text;

            return View();
        }
    }
}
