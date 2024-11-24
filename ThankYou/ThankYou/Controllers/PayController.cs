using Microsoft.AspNetCore.Mvc;
using ThankYou.ViewModels;

namespace ThankYou.Controllers
{
    public class PayController : Controller
    {
        [HttpPost]
        public IActionResult SendTip(PayViewModel model)
        {
            freturn View();
        }
    }
}
