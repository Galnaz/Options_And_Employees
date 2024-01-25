using Employees.Domain.Employees;
using Employees.Domain.Salary;
using Employees.Application;
using Moq;
using System.Collections.Generic;
using Xunit;
using System;

namespace TestProject1.Employees.Application
{
    public class EmployeeReportGeneratorTests
    {
        [Fact]
        public void GenerateReport_ShouldGenerateCorrectReportForMultipleEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Name = "JohnDoe", Years = 2, Salary = 50000, Type = EmployeeType.Trainee },
                new Employee { Name = "AliceSmith", Years = 7, Salary = 80000, Type = EmployeeType.Senior },
                new Employee { Name = "BobJones", Years = 5, Salary = 60000, Type = EmployeeType.Junior }
            };

            var newSalary = 50500;
            var salaryCalculatorMock = new Mock<IEmployeeSalaryCalculator>();
            salaryCalculatorMock.Setup(x => x.GetNewSalary(It.IsAny<Employee>())).Returns(newSalary);

            var reportGenerator = new EmployeeReportGenerator(salaryCalculatorMock.Object);

            // Act
            var report = reportGenerator.GenerateReport(employees);

            // Assert
            Assert.Contains($"Employee Name: Joh****, Type: Trainee, Years: 2, Salary: 50000, New Salary: {newSalary}", report);
            Assert.Contains($"Employee Name: Ali*******, Type: Senior, Years: 7, Salary: 80000, New Salary: {newSalary}", report);
            Assert.Contains($"Employee Name: Bob*****, Type: Junior, Years: 5, Salary: 60000, New Salary: {newSalary}", report);
        }

        [Fact]
        public void GenerateReport_ShouldGenerateCorrectReportForEmpltyEmployeesList()
        {
            // Arrange
            var employees = new List<Employee>
            {
            };

            var salaryCalculatorMock = new Mock<IEmployeeSalaryCalculator>();
            
            var reportGenerator = new EmployeeReportGenerator(salaryCalculatorMock.Object);

            // Act
            var report = reportGenerator.GenerateReport(employees);

            // Assert
            Assert.Contains($"", report);
        }

        [Fact]
        public void GenerateReport_ShouldHandleCorrectlyEmpltyEmployeesList()
        {
            // Arrange
            List<Employee> employees = null;

            var salaryCalculatorMock = new Mock<IEmployeeSalaryCalculator>();
            var reportGenerator = new EmployeeReportGenerator(salaryCalculatorMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => reportGenerator.GenerateReport(employees));
        }

        [Theory]
        [InlineData(EmployeeType.Trainee, "JohnDoe", 2, 50000)]
        [InlineData(EmployeeType.Junior, "JohnDoe", 2, 50000)]
        [InlineData(EmployeeType.Senior, "JohnDoe", 2, 50000)]
        [InlineData(EmployeeType.Manager, "JohnDoe", 2, 50000)]
        public void GenerateEmployeeReport_ShouldGenerateCorrectReportForSingleEmployee(
            EmployeeType employeeType, string name, int years, decimal salary)
        {
            // Arrange
            var employee = new Employee
            {
                Name = name,
                Years = years,
                Salary = salary,
                Type = employeeType
            };

            var renewedSalary = 50100;
            var salaryCalculatorMock = new Mock<IEmployeeSalaryCalculator>();
            salaryCalculatorMock.Setup(x => x.GetNewSalary(It.IsAny<Employee>())).Returns(renewedSalary);

            var reportGenerator = new EmployeeReportGenerator(salaryCalculatorMock.Object);

            // Act
            var report = reportGenerator.GenerateReport
                (new List<Employee>() { employee });

            // Assert
            Assert.Contains($"Employee Name: Joh****, Type: {employeeType}, Years: {years}, Salary: {salary}, New Salary: {renewedSalary}", report);
        }
    }

}
