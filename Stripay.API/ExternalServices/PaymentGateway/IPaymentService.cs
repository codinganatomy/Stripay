namespace Stripay.API.ExternalServices.PaymentGateway;

public interface IPaymentService
{
    Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
}
