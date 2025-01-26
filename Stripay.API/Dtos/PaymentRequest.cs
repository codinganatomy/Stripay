namespace Stripay.API.Dtos;

public class PaymentRequest
{
    public required string CustomerEmail { get; set; }
    public required string PaymentMethodId { get; set; }
    public decimal Amount { get; set; }
    public required string CardName { get; set; }
    public int CardExpiryMonth { get; set; }
    public int CardExpiryYear { get; set; }
    public required string CardLast4Digits { get; set; }
}
