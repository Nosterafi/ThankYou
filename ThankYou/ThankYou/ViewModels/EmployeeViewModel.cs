using ThankYou.DB.Context;
using ThankYou.DB.Domain;

namespace ThankYou.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }

        public DateOnly Day { get; set; }

        public int SumTips 
        {
            get
            {
                return PostgresContext
                    .Current.Tips.Where(t => t.Employee.Id == Employee.Id && t.Date == Day)
                    .Sum(t => t.Sum);
            }
        }

        public EmployeeViewModel(Employee employee)
        {
            Employee = employee;
        }
    }
}
