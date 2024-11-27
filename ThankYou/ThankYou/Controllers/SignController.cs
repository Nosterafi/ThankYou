using Microsoft.AspNetCore.Mvc;

namespace ThankYou.Controllers
{
    public class SignController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
