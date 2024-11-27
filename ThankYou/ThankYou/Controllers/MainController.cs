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
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger) : base()
        {
            _logger = logger;
        }

        public IActionResult Index() 
        { 
            return View("main"); 
        }

        public IActionResult PayPage(short employeeId)
        {
            var employee = PostgresContext.Current.Employees.Find(employeeId);

            if (employee == null)
            {
                HttpContext.Session.SetString("invEmpFlag","true");
                return RedirectToAction("Index");
            }

            employee.Merchant = PostgresContext.Current.Merchants.Find(employee.MerchantId);

            var model = new PayViewModel(new Tip { Employee = employee , EmployeeId = employee.Id});
            return View("pay", model);
        }

        //[HttpPost]
        //public ActionResult SignUp()
        //{
        //    throw new NotImplementedException();
        //    return View("signUp");
        //}

        //[HttpPost]
        //public ActionResult SignIn()
        //{
        //    return View("signIn");
        //}

        

        //public IActionResult EmployeeProfile(short id)
        //{
        //    var employee = _postgresContext.Employees.Find(id);
        //    employee.Merchant = _postgresContext.Merchants.Find(employee.MerchantId);

        //    return View("EmployeeProfile", employee);
        //}

        //[HttpPost]
        //public IActionResult FindEmployee(short employeeId)
        //{
        //    var employee = _postgresContext.Employees.Find(employeeId);

        //    if (employee == null)
        //    {
        //        HttpContext.Session.SetString("invEmpFlag","true");
        //        return RedirectToAction("MainPage");
        //    }

        //    employee.Merchant = _postgresContext.Merchants.Find(employee.MerchantId);

        //    return View("EmployPage");
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
