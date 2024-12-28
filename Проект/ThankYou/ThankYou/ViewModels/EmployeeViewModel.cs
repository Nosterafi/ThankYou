using ThankYou.DB.Context;
using ThankYou.DB.Domain;

namespace ThankYou.ViewModels
{
    public class EmployeeViewModel
    {
        private DateOnly day { get; set; }

        public bool OpenStatFlag { get; set; } = false;

        public string FormatedDay
        {
            get { return day.ToString("yyyy-MM-dd"); }

            set
            {
                day = DateOnly.Parse(value);
            }
        }

        public Employee Employee { get; set; }

        public int SumTips 
        {
            get
            {
                return PostgresContext
                    .Current.Tips.Where(t => t.Employee.Id == Employee.Id && t.Date == day)
                    .Sum(t => t.Sum);
            }
        }

        public EmployeeViewModel()
        {
            Employee = new Employee();
        }

        public EmployeeViewModel(Employee employee)
        {
            Employee = employee;
        }
    }
}
