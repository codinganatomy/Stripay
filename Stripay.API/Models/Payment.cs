namespace Stripay.API.Models;

public class Payment
{
    public int PaymentId { get; set; }
    public required string StripeCustomerId { get; set; }
    public required string PaymentMethodId { get; set; }
    public string? ChargeId { get; set; } = null;
    public PaymentStatus PaymentStatus { get; set; }
    public decimal Amount { get; set; }
    public required string CardName { get; set; }
    public int CardExpiryMonth { get; set; }
    public int CardExpiryYear { get; set; }
    public required string CardLast4Digits { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}

public enum PaymentStatus
{
    Pending = 1,
    Cancelled = 2,
    Payed = 3,
}
