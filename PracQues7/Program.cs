using System;
namespace PracQues7
{
    public class Program
    {
        public class Employee
        {
            public string Name;
            public double HoursWorked;
            public double HourlyRate;
            public Employee(string name, double hoursWorked, double hourlyRate)
            {
                Name = name;
                HoursWorked = hoursWorked;
                HourlyRate = hourlyRate;
            }
        }
        public class PayrollCalculator
        {
            private const double RegularHoursLimit = 40;
            private const double OvertimeMultiplier = 1.5;

            public double CalculateRegularPay(Employee employee)
            {
                double regularHours = Math.Min(RegularHoursLimit, employee.HoursWorked);
                return regularHours * employee.HourlyRate;
            }
            public double CalculateOvertimePay(Employee employee)
            {
                double overtimeHours = Math.Max(employee.HoursWorked - RegularHoursLimit, 0);
                double overtimeRate = employee.HourlyRate * OvertimeMultiplier;
                return overtimeHours * overtimeRate;
            }
            public double CalculateGrossSalary(Employee employee)
            {
                double regularPay = CalculateRegularPay(employee);
                double overtimePay = CalculateOvertimePay(employee);
                return regularPay + overtimePay;
            }
        }
        public static void Main(string[] args)
        {
            string employeeName;
            double hoursWorked;
            double hourlyRate;

            while (true)
            {
                Console.WriteLine("Enter Employee name: ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    employeeName = input;
                    break;
                }
                else
                {
                    Console.WriteLine("Error: Name cannot be empty");
                }
            }

            while (true)
            {
                Console.WriteLine("Enter the Hours Worked: ");
                string? input = Console.ReadLine();

                if (double.TryParse(input, out hoursWorked))
                {
                    if (hoursWorked >= 0 && hoursWorked <= 168)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Hours worked must be between 0 and 168.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid numeric value.");
                }
            }

            while(true)
            {
                Console.WriteLine("Enter the Hourly Rate: ");
                string? input = Console.ReadLine();

                if(double.TryParse(input, out hourlyRate))
                {
                    if(hourlyRate > 0 && hourlyRate <= 10000)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Hourly rate must be greater than 0 and not exceed £10,000.");
                    }
                }
                else
                {
                    Console.WriteLine( "Error: Please enter a valid numeric value.");
                }
            }

            Employee employee = new Employee(employeeName, hoursWorked, hourlyRate);
            PayrollCalculator calculator = new PayrollCalculator();
            double regularPay = Math.Round(calculator.CalculateRegularPay(employee),2);
            double overtimePay = Math.Round(calculator.CalculateOvertimePay(employee),2);
            double grossSalary = Math.Round(calculator.CalculateGrossSalary(employee),2);

            Console.WriteLine("---PAYROLL SUMMARY---");

            Console.WriteLine($"Employee name: {employeeName}");
            Console.WriteLine($"Hours Worked: {hoursWorked} hrs");
            Console.WriteLine($"Regulary Pay: Rs.{regularPay}");
            Console.WriteLine($"Overtime Pay: Rs.{overtimePay}");
            Console.WriteLine($"Gross Salary: Rs.{grossSalary}");
        }
    }
}