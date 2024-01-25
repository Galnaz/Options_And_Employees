using Employees.Domain.Employees;
using Employees.Domain.Salary;
using System;
using Xunit;

namespace TestProject1.Employees.Domain
{
    public class EmployeeSalaryCalculatorTests
    {
        [Theory]
        [InlineData(EmployeeType.Trainee, 50000, 50500)] // salary / 100 + salary
        [InlineData(EmployeeType.Junior, 50000, 52500)] // (salary * 5 / 100) + (salary * years > 5 ? 5 : years) / 100 + salary
        [InlineData(EmployeeType.Senior, 50000, 55000)] // (salary * years > 5 ? 5 : years) / 100 + 1.1 * salary
        [InlineData(EmployeeType.Manager, 50000, 57500)] // (salary * years > 5 ? 5 : years) / 100 + salary + (salary * 15 / 100)
        public void GetNewSalary_ShouldCalculateSalaryCorrectly(EmployeeType type, decimal baseSalary, decimal expectedNewSalary)
        {
            // Arrange
            var calculator = new EmployeeSalaryCalculator();
            var employee = new Employee
            {
                Name = "JohnDoe",
                Years = 6,
                Salary = baseSalary,
                Type = type
            };

            // Act
            var newSalary = calculator.GetNewSalary(employee);

            // Assert
            Assert.Equal(expectedNewSalary, newSalary);
        }

        [Fact]
        public void GetNewSalary_ShouldThrowExceptionForInvalidEmployeeType()
        {
            // Arrange
            var calculator = new EmployeeSalaryCalculator();
            var invalidEmployee = new Employee
            {
                Name = "JohnDoe",
                Years = 2,
                Salary = 50000,
                Type = (EmployeeType)10
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => calculator.GetNewSalary(invalidEmployee));
        }
    }
}
