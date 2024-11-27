using Microsoft.AspNetCore.Mvc;
using ThankYou.DB.Context;
using ThankYou.DB.Domain;

namespace ThankYou.Controllers
{
    public class SignController : Controller
    {
        [HttpPost]
        public IActionResult SignIn()
        {
            return View("signIn");
        }

        [HttpPost]
        public IActionResult FindEmployee(string phoneNumber, string password)
        {
            var employee = PostgresContext.Current.Employees
                .FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber) && x.Password.Equals(password));

            if (employee == null)
            {
                
            }

            employee.Merchant = PostgresContext.Current.Merchants.Find(employee.MerchantId);

            return View("EmployPage");
        }
    }
}
