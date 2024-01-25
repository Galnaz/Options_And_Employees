
namespace Employees.Domain.Employees
{
    public class Employee
    {
        public string Name { get; set; }
        public int Years { get; set; }
        public decimal Salary { get; set; }
        public EmployeeType Type { get; set; }

        public string MaskName()
        {
            if(Name.Length <= 3)
                return Name;

            var firstChars = Name.Substring(0, 3);
            var length = Name.Length - 3;

            for (int i = 0; i < length; i++)
            {
                firstChars += "*";
            }

            return firstChars;
        }
    }

    public enum EmployeeType
    {
        Trainee,
        Junior,
        Senior,
        Manager
    }
}
