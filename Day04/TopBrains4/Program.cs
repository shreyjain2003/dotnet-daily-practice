using System;

public class ArithmeticExpressions

{
    public string EvaluateExpression(string expression)
    {
        string[] parts = expression.Split(' ');

        if (parts.Length != 3)
            return "Error:InvalidExpression";

        if (!int.TryParse(parts[0], out int a) || !int.TryParse(parts[2], out int b))
            return "Error:InvalidNumber";

        switch (parts[1])
        {
            case "+":
                return (a + b).ToString();

            case "-":
                return (a - b).ToString();

            case "*":
                return (a * b).ToString();

            case "/":
                if (b == 0)
                    return "Error:DivideByZero";
                return (a / b).ToString();

            default:
                return "Error:UnknownOperator";
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the expression along with the space in-between.");
        string expression = Console.ReadLine();

        ArithmeticExpressions solution = new ArithmeticExpressions();
        Console.WriteLine(solution.EvaluateExpression(expression));
    }
}