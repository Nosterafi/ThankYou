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
            // Проверка наличия пользователя в базе данных
            // Необходимо как то различать роли пользователей у нас есть сотрудники, а есть клиенты, а еще как бы же есть заведения
            // Пока что во избежании ошибок ишем пользователей только в сотрудниках. Временное решение 
            var user = PostgresContext.Current.Employees
                .FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password);

            if (user != null)
            {
                // Если пользователь найден, перенаправляем на страницу профиля, передавая id для поиска информации о нем
                user.Merchant = PostgresContext.Current.Merchants.Find(user.MerchantId);
                return View("EmployeeProfile", user);
            }
            else
            {
                // Если пользователь не найден, возвращаем сообщение об ошибке
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
                return View("signIn"); // Возвращаем текущее представление с ошибкой
            }
        }
    }
}
