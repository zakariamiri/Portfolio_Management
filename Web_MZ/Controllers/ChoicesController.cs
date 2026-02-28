using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_MZ.Controllers
{
    public class ChoicesController : Controller
    {
        public ActionResult choices()
        {
            return View();
        }
    }
}
