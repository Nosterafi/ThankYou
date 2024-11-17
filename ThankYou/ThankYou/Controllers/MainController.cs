using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThankYou.Models;
using ThankYou.DB.Context;
using ThankYou.DB.Domain;

namespace ThankYou.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        private readonly PostgresContext _postgresContext = new();

        private Tip tip;

        public Employee Employee { get; set; }

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        public IActionResult MainPage() { return View("main"); }

        public IActionResult PayPage(short employeeId)
        {
            var employee = _postgresContext.Employees.Find(employeeId);
            employee.Merchant = _postgresContext.Merchants.Find(employee.MerchantId);

            tip = new Tip();
            tip.Employee = employee;
            tip.EmployeeId = employeeId;

            return View("pay", tip);
        }

        [HttpPost]
        public IActionResult SetSum(short sum) 
        { 
            tip.Sum = sum;

            return RedirectToAction("PayPage", tip.EmployeeId);
        }

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
