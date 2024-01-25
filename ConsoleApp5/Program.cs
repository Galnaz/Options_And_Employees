// See https://aka.ms/new-console-template for more information


using Employees.Application;
using Employees.Domain.Employees;
using Employees.Domain.Salary;

var reportsGenerator = new EmployeeReportGenerator(new EmployeeSalaryCalculator());
Console.WriteLine(reportsGenerator.GenerateReport(new List<Employee>()
{
    new Employee
    {
        Name = "JohnDow",
        Salary = 5000,
        Type = EmployeeType.Senior,
        Years = 5
    },
    new Employee
    {
        Salary = 10000,
        Name = "Barbara",
        Years = 10,
        Type = EmployeeType.Manager
    }
}));