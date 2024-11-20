using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThankYou.Models;
using ThankYou.DB.Context;
using ThankYou.DB.Domain;
using ThankYou.ViewModels;
using Newtonsoft.Json;

namespace ThankYou.Controllers
{
    public class MainController : Controller
    {
        private static PostgresContext _postgresContext = new();

        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger) : base()
        {
            _logger = logger;
        }

        public IActionResult Index(short employeeId = -1) 
        {
            HttpContext.Session.SetString("invEmpFlag", "false");
            return MainPage(employeeId);
        }

        public IActionResult MainPage(short employeeId = -1) { return View("main", employeeId); }

        [HttpPost]
        public IActionResult PayPage(short employeeId)
        {
            var employee = _postgresContext.Employees.Find(employeeId);

            if (employee == null)
            {
                HttpContext.Session.SetString("invEmpFlag","true");
                return RedirectToAction("MainPage");
            }

            employee.Merchant = _postgresContext.Merchants.Find(employee.MerchantId);

            var tip = new Tip();
            tip.Employee = _postgresContext.Employees.Find(employeeId);
            tip.EmployeeId = employee.Id;

            var tipJson = JsonConvert.SerializeObject(tip);
            HttpContext.Session.SetString("currentTip", tipJson);

            return View("pay", new PayViewModel(tip));
        }

        [HttpPost]
        public IActionResult SendTip(PayViewModel viewModel) 
        {
            var currentTip = JsonConvert.DeserializeObject<Tip>(HttpContext.Session.GetString("currentTip"));

            currentTip.Employee = _postgresContext.Employees.Find(currentTip.EmployeeId);
            
            currentTip.Sum = viewModel.Tip.Sum;
            currentTip.Grade = viewModel.Tip.Grade;
            currentTip.Review = viewModel.Tip.Review;
            currentTip.Date = DateOnly.FromDateTime(DateTime.Now);

            _postgresContext.Tips.Add(currentTip);
            _postgresContext.SaveChanges();
            _postgresContext = new();

            return View("PaySucces");
        }


        public ActionResult SignUp()
        {
            throw new NotImplementedException();
            return View("signUp");
        }

        [HttpPost]
        public ActionResult SignIn()
        {
            return View("signIn");
        }

        public IActionResult Employee(short employeeId)
        {
            var employee = _postgresContext.Employees.Find(employeeId);

            if (employee == null)
            {
                HttpContext.Session.SetString("invEmpFlag","true");
                return RedirectToAction("MainPage");
            }

            employee.Merchant = _postgresContext.Merchants.Find(employee.MerchantId);

            return View("EmployPage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
