using Microsoft.AspNetCore.Mvc;
using ThankYou.DB.Context;
using ThankYou.ViewModels;

namespace ThankYou.Controllers
{
    public class PayController : Controller
    {
        [HttpPost]
        public IActionResult SendTip(PayViewModel model)
        {
            if (PostgresContext.Current.BankCards.FirstOrDefault(x => x.Owner == model.Tip.EmployeeId) == null)
                return View("PayError");

            model.Tip.Date = DateOnly.FromDateTime(DateTime.Now);

            PostgresContext.Current.Tips.Add(model.Tip);
            PostgresContext.Current.SaveChanges();
            PostgresContext.Current = new PostgresContext();

            return View("PaySucces");
        }
    }
}
