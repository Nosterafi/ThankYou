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

        public IActionResult Index() 
        {
            HttpContext.Session.SetString("invEmpFlag", "false");
            return MainPage();
        }

        public IActionResult MainPage() { return View("main"); }

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

        public IActionResult SendTip(PayViewModel viewModel) 
        {
            var currentTip = JsonConvert.DeserializeObject<Tip>(HttpContext.Session.GetString("currentTip"));
            var card = _postgresContext.BankCards.FirstOrDefault(card => card.Owner == currentTip.EmployeeId);

            if (card == null)
                return View("payError");

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
            // �������� ������� ������������ � ���� ������
            // ���������� ��� �� ��������� ���� ������������� � ��� ���� ����������, � ���� �������, � ��� ��� �� �� ���� ���������
            // ���� ��� �� ��������� ������ ���� ������������� ������ � �����������. ��������� ������� 
            var user = _postgresContext.Employees
                .FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password); 

            if (user != null)
            {
                // ���� ������������ ������, �������������� �� �������� �������, ��������� id ��� ������ ���������� � ���
                return RedirectToAction("EmployeeProfile", new { id = user.Id});
            }
            else
            {
                // ���� ������������ �� ������, ���������� ��������� �� ������
                ModelState.AddModelError(string.Empty, "�������� ����� ��� ������.");
                return View("signIn"); // ���������� ������� ������������� � �������
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
