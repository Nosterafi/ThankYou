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

        [HttpPost]
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

        [HttpPost]
        public IActionResult Employee(string phoneNumber, string password)
        {
            // ѕроверка наличи€ пользовател€ в базе данных
            // Ќеобходимо как то различать роли пользователей у нас есть сотрудники, а есть клиенты, а еще как бы же есть заведени€
            var user = _postgresContext.Users
                .FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password); 

            if (user != null)
            {
                // ≈сли пользователь найден, перенаправл€ем на страницу профил€, передава€ id дл€ поиска информации о нем
                return RedirectToAction("EmployeeProfile", new { id = user.Id});
            }
            else
            {
                // ≈сли пользователь не найден, возвращаем сообщение об ошибке
                ModelState.AddModelError(string.Empty, "Ќеверный логин или пароль.");
                return View("signIn"); // ¬озвращаем текущее представление с ошибкой
            }
        }

        public IActionResult EmployeeProfile(short id)
        {
            var employee = _postgresContext.Employees.Find(id);
            employee.Merchant = _postgresContext.Merchants.Find(employee.MerchantId);

            return View("EmployeeProfile", employee);
        }

        [HttpPost]
        public IActionResult FindEmployee(short employeeId)
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
