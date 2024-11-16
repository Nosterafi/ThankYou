using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThankYou.Models;

namespace ThankYou.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        public IActionResult MainPage() { return View("main"); }

        public IActionResult PayPage() { return View("pay"); }

        public IActionResult PayErrorPage() { return View("payError"); }

        public IActionResult PayNotenoughPage() { return View("payNotenough"); }

        public IActionResult PaySuccesPage() { return View("paySucces"); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
