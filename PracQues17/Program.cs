using System;

namespace PracQues17
{
    // =========================================
    // EMPLOYEE CLASS
    // =========================================

    public class Employee
    {
        public string Name { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }

        public Employee(
            string name,
            double hoursWorked,
            double hourlyRate)
        {
            Name = name;
            HoursWorked = hoursWorked;
            HourlyRate = hourlyRate;
        }
    }


    // =========================================
    // PAYROLL CALCULATOR
    // =========================================

    public class PayrollCalculator
    {
        private const double RegularHoursLimit = 40;
        private const double OvertimeMultiplier = 1.5;


        // -----------------------------------------
        // CALCULATE REGULAR PAY
        // -----------------------------------------

        public double CalculateRegularPay(
            Employee employee)
        {
            double regularHours =
                Math.Min(
                    employee.HoursWorked,
                    RegularHoursLimit
                );

            return regularHours *
                   employee.HourlyRate;
        }


        // -----------------------------------------
        // CALCULATE OVERTIME PAY
        // -----------------------------------------

        public double CalculateOvertimePay(
            Employee employee)
        {
            double overtimeHours =
                Math.Max(
                    employee.HoursWorked -
                    RegularHoursLimit,
                    0
                );

            double overtimeRate =
                employee.HourlyRate *
                OvertimeMultiplier;

            return overtimeHours *
                   overtimeRate;
        }


        // -----------------------------------------
        // CALCULATE GROSS SALARY
        // -----------------------------------------

        public double CalculateGrossSalary(
            Employee employee)
        {
            double regularPay =
                CalculateRegularPay(employee);

            double overtimePay =
                CalculateOvertimePay(employee);

            return regularPay +
                   overtimePay;
        }
    }


    // =========================================
    // PROGRAM
    // =========================================

    public class Program
    {
        public static void Main(string[] args)
        {
            string employeeName;
            double hoursWorked;
            double hourlyRate;


            // =========================================
            // EMPLOYEE NAME
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Employee Name: "
                );

                employeeName =
                    Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(
                    employeeName))
                {
                    break;
                }

                Console.WriteLine(
                    "Error: Employee name cannot be empty."
                );
            }


            // =========================================
            // HOURS WORKED
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Hours Worked: "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out hoursWorked))
                {
                    if (double.IsFinite(hoursWorked) &&
                        hoursWorked >= 0 &&
                        hoursWorked <= 744)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Hours must be between 0 and 744."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric value."
                    );
                }
            }


            // =========================================
            // HOURLY RATE
            // =========================================

            while (true)
            {
                Console.Write(
                    "Enter Hourly Rate (£): "
                );

                string? input =
                    Console.ReadLine();

                if (double.TryParse(
                    input,
                    out hourlyRate))
                {
                    if (double.IsFinite(hourlyRate) &&
                        hourlyRate > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine(
                            "Error: Hourly rate must be greater than zero."
                        );
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Error: Please enter a valid numeric hourly rate."
                    );
                }
            }


            // =========================================
            // CREATE EMPLOYEE OBJECT
            // =========================================

            Employee employee =
                new Employee(
                    employeeName,
                    hoursWorked,
                    hourlyRate
                );


            // =========================================
            // CREATE PAYROLL CALCULATOR
            // =========================================

            PayrollCalculator calculator =
                new PayrollCalculator();


            // =========================================
            // CALCULATE PAY
            // =========================================

            double regularPay =
                calculator.CalculateRegularPay(
                    employee
                );

            double overtimePay =
                calculator.CalculateOvertimePay(
                    employee
                );

            double grossSalary =
                calculator.CalculateGrossSalary(
                    employee
                );


            // =========================================
            // VALIDATE CALCULATED VALUES
            // =========================================

            if (!double.IsFinite(regularPay) ||
                !double.IsFinite(overtimePay) ||
                !double.IsFinite(grossSalary))
            {
                Console.WriteLine(
                    "Error: Unable to calculate a valid salary."
                );

                return;
            }


            // =========================================
            // ROUND RESULTS
            // =========================================

            regularPay =
                Math.Round(regularPay, 2);

            overtimePay =
                Math.Round(overtimePay, 2);

            grossSalary =
                Math.Round(grossSalary, 2);


            // =========================================
            // DISPLAY PAYROLL
            // =========================================

            Console.WriteLine(
                "\n--- PAYROLL SUMMARY ---"
            );

            Console.WriteLine(
                $"Employee Name: {employee.Name}"
            );

            Console.WriteLine(
                $"Hours Worked: {employee.HoursWorked:F2}"
            );

            Console.WriteLine(
                $"Hourly Rate: £{employee.HourlyRate:F2}"
            );

            Console.WriteLine(
                $"Regular Pay: £{regularPay:F2}"
            );

            Console.WriteLine(
                $"Overtime Pay: £{overtimePay:F2}"
            );

            Console.WriteLine(
                $"Gross Salary: £{grossSalary:F2}"
            );
        }
    }
}