using Employees.Domain.Employees;
using Employees.Domain.Salary;

namespace Employees.Application
{
    public interface IEmployeeReportGenerator
    {
        string GenerateReport(List<Employee> employees);
    }

    public class EmployeeReportGenerator : IEmployeeReportGenerator
    {
        private readonly IEmployeeSalaryCalculator _employeeSalaryCalculator;

        public EmployeeReportGenerator(IEmployeeSalaryCalculator employeeSalaryCalculator)
        {
            _employeeSalaryCalculator = employeeSalaryCalculator;
        }

        private string GenerateEmployeeReport(Employee employee)
        {
            var newSalary = _employeeSalaryCalculator.GetNewSalary(employee);
            return
            $"Employee Name: {employee.MaskName()}, Type: {employee.Type}, " +
             $"Years: {employee.Years}, Salary: {employee.Salary}, " +
             $"New Salary: {newSalary}";
        }

        public string GenerateReport(List<Employee> employees)
        {
            if(employees == null)
                throw new ArgumentNullException(nameof(employees));

            var report = "";
            foreach (var employee in employees)
            {
                report += GenerateEmployeeReport(employee);
            }
            return report;
        }
    }
}
