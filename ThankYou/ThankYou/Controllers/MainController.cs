using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThankYou.Models;
using ThankYou.DB.Context;
using ThankYou.DB.Domain;
using ThankYou.ViewModels;

namespace ThankYou.Controllers
{
    public class MainController : Controller
    {
        private static readonly PostgresContext _postgresContext = new();

        private readonly ILogger<MainController> _logger;

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

            var tip = new Tip();
            tip.Employee = employee;

            return View("pay", new PayViewModel(tip));
        }

        [HttpPost]
        public IActionResult SendTip(PayViewModel viewModel) 
        {
            viewModel.Tip.Employee = employee;
            
            _postgresContext.Tips.Add(viewModel.Tip);
            _postgresContext.SaveChanges();

            return RedirectToAction("MainPage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
