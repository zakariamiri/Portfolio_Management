using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_MZ.Controllers
{
    public class DemonController : Controller
    {
        public IActionResult FirstDemon()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("UserFirstName");
            return View();
        }
        public IActionResult SecondDemon()
        {
            return View();
        }
        public IActionResult ThirdDemon()
        {
            return View();
        }
    }
}
