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

            HttpContext.Session.SetString("invEmpFlag", "false");

            employee.Merchant = PostgresContext.Current.Merchants.Find(employee.MerchantId);

            var model = new PayViewModel(new Tip { Employee = employee , EmployeeId = employee.Id});
            return View("pay", model);
        }
    }
}
