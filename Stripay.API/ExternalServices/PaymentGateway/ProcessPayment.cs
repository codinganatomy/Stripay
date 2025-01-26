namespace Stripay.API.ExternalServices.PaymentGateway;

public class ProcessPaymentRequest
{
    public required string CustomerEmail { get; set; }
    public required string CustomerName { get; set; }
    public required string PaymentMethodId { get; set; }
    public long Amount { get; set; }
    public required string Currency { get; set; }
    public bool CapturePayment { get; set; }
    public List<string> PaymentMethodTypes { get; set; } = [];
}

public class ProcessPaymentResponse
{
    public int ResponseStatusCode { get; set; }
    public required string CustomerId { get; set; }
    public decimal Amount { get; set; }
    public required string PaymentIntentId { get; set; }
    public  required string CaptureMethod { get; set; }
    public required string ChargeId { get; set; }
    public required string PaymentMethodId { get; set; }
    public List<string> PaymentMethodTypes { get; set; } = [];
    public required string Status { get; set; }
}