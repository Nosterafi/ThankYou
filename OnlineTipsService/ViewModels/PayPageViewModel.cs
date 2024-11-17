using Microsoft.AspNetCore.Identity;
using ThankYouDB.Domain;

namespace ThankYou.ViewModels
{
    public class PayPageViewModel
    {
        Tip Tip { get; set; }
        public PayPageViewModel(short employeeId) 
        {
            Tip = new Tip();
            Tip.EmployeeId = employeeId;
        }
        public void CallPaymentGateway()
        {
            if (Tip.Sum == 0)
                throw new Exception("Не указана сумма");
            if (Tip.Grade == 0)
                throw new Exception("Не указана оценка");

            //Дальше вызываем платежный шлюз и передаем туда информацию

            throw new NotImplementedException();
        }
    }
}
