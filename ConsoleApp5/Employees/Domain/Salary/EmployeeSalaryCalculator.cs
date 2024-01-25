using Employees.Domain.Employees;

namespace Employees.Domain.Salary
{
    /// <summary>
    /// Domain service to hold logic for salary calculations
    /// </summary>
    public interface IEmployeeSalaryCalculator
    {
        /// <summary>
        /// Re-calculate Salary for Employee based on it's type 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        decimal GetNewSalary(Employee employee);
    }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class EmployeeSalaryCalculator : IEmployeeSalaryCalculator
    {

        public decimal GetNewSalary(Employee employee)
        {
            switch (employee.Type)
            {
                case EmployeeType.Trainee:
                    return employee.Salary / 100 + employee.Salary;
                case EmployeeType.Junior:
                    return (employee.Salary * 5 / 100) +
                        (employee.Salary * employee.Years > 5 ? 5 : employee.Years)
                        / 100 + employee.Salary;
                case EmployeeType.Senior:
                    return (employee.Salary * employee.Years > 5 ? 5 : employee.Years)
                        / 100 + 1.1m * employee.Salary;
                case EmployeeType.Manager:
                    return (employee.Salary * employee.Years > 5 ? 5 : employee.Years)
                        / 100 + employee.Salary + (employee.Salary * 15 / 100);
                default:
                    throw new ArgumentException("Invalid Employee type");
            }

        }
    }
}
