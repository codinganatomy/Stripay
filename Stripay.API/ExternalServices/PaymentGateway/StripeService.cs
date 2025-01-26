using Stripe;

namespace Stripay.API.ExternalServices.PaymentGateway;

public class StripeService(CustomerService customerService, PaymentIntentService paymentIntentService) : IPaymentService
{
    private readonly CustomerService _customerService = customerService;
    private readonly PaymentIntentService _paymentIntentService = paymentIntentService;

    public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
    {
         
            var customerId = await CreateCustomer(request.CustomerEmail, request.CustomerName);

            var intentCreateOptions = new PaymentIntentCreateOptions
        {
            Customer = customerId,
            Amount = request.Amount,
            Currency = request.Currency,
            Confirm = request.CapturePayment,
            PaymentMethodTypes = request.PaymentMethodTypes,
            PaymentMethod = request.PaymentMethodId,
        };
            var response = await _paymentIntentService.CreateAsync(intentCreateOptions);

            return new ProcessPaymentResponse
            {
                ResponseStatusCode = (int)response.StripeResponse.StatusCode,
                Amount = response.Amount,
                PaymentIntentId = response.Id,
                PaymentMethodId = response.PaymentMethodId,
                CaptureMethod = response.CaptureMethod,
                CustomerId = response.CustomerId,
                ChargeId = response.LatestChargeId,
                Status = response.Status,
                PaymentMethodTypes = response.PaymentMethodTypes,
            };
        
    }

    public async Task<string> CreateCustomer(
        string email,
        string name
    )
    {
            var customerOptions = new CustomerCreateOptions { Email = email, Name = name };
            Customer customer = await _customerService.CreateAsync(
                options: customerOptions,
                requestOptions: null
            );

            return customer.Id; 
    }
}
