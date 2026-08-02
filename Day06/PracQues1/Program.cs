using System;

public class ParkingFeeCalculator
{
    public class Calculator
    {
        public string VehicleType;
        public double ParkingHours;

        public Calculator(string vehicleType, double parkingHours)
        {
            VehicleType = vehicleType;
            ParkingHours = parkingHours;
        }

        public double Fee()
        {
            double fee = 0;
            double actualHours = ParkingHours;

            // First 30 minutes free
            if (actualHours <= 0.5)
                return 0;

            // Charge only after first 30 minutes
            ParkingHours -= 0.5;

            switch (VehicleType.ToUpper())
            {
                case "C":
                    fee = Math.Min(ParkingHours * 3, 25);
                    break;

                case "M":
                    fee = Math.Min(ParkingHours * 2, 15);
                    break;

                case "T":
                    fee = Math.Min(ParkingHours * 5, 40);
                    break;

                default:
                    Console.WriteLine("Invalid Vehicle Type");
                    return 0;
            }

            // Discount based on actual parking time
            if (actualHours > 8)
            {
                fee -= fee * 0.10;
            }

            return fee;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter Vehicle Type (C/M/T): ");
            string vehicleType = Console.ReadLine();

            Console.Write("Enter Parking Hours: ");
            double parkingHours = Convert.ToDouble(Console.ReadLine());

            Calculator calculator = new Calculator(vehicleType, parkingHours);

            double finalFee = calculator.Fee();

            Console.WriteLine("\n----- Parking Bill -----");
            Console.WriteLine($"Vehicle Type   : {vehicleType.ToUpper()}");
            Console.WriteLine($"Parking Hours  : {parkingHours}");
            Console.WriteLine($"Total Fee      : ${finalFee:F2}");
        }
    }
}