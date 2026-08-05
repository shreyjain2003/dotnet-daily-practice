using System;

namespace PaymentProcessingSystem
{
    public class PaymentException : Exception
    {
        public string PaymentGateway {get; }
        public decimal Amount {get; }
        public DateTime Timestamp {get; }

        public PaymentException(string message, string gateway, decimal amount) : base(message)
        {
            PaymentGateway = gateway;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        } 
        public virtual string GetDetailedInfo()
        {
            return $"[{Timestamp}] {PaymentGateway} : {Message}";
        }
    }
    public class InsufficientFundsException : PaymentException
    {
        public decimal CurrentBalance {get;}
        public InsufficientFundsException(string gateway, decimal amount, decimal balance) : base(
            $"Insufficient funds. Required: {amount:C}, Available: {balance:C}", gateway, amount)
        {
            CurrentBalance = balance;
        }
    }
    public class InvalidCardException : PaymentException
    {
        public string CardNumber { get; }
        public InvalidCardException(
            string gateway,
            decimal amount,
            string cardNumber) : base("Invalid card number",gateway,amount)
        {
            CardNumber = cardNumber;
        }
    }
    public interface IPaymentGateway
    {
        string GatewayName {get ;}
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
        bool ValidatePayment(PaymentRequest request);
    }
    public record PaymentRequest(decimal Amount, string CardNumber, string CardHolder);
    public record PaymentResult(string TransactionId, bool IsSuccess, string Message);

    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}