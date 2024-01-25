

using Employees.Domain.Employees;
using Xunit;

namespace TestProject1.Employees.Domain
{
    public class EmployeeTests
    {
        [Theory]
        [InlineData("JohnDoe", 2, 50000, EmployeeType.Junior, "Joh****")]
        [InlineData("AliceSmith", 4, 80000, EmployeeType.Senior, "Ali*******")]
        [InlineData("", 1, 10000, EmployeeType.Trainee, "")]
        public void MaskName_ShouldMaskNameCorrectly_WithDifferentInputs(string name, int years, decimal salary, EmployeeType type, string expectedMaskedName)
        {
            // Arrange
            var employee = new Employee
            {
                Name = name,
                Years = years,
                Salary = salary,
                Type = type
            };

            // Act
            var maskedName = employee.MaskName();

            // Assert
            Assert.Equal(expectedMaskedName, maskedName);
        }
    }
}
