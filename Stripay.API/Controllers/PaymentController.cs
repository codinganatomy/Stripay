using Microsoft.AspNetCore.Mvc;
using Stripay.API.Dtos;
using Stripay.API.ExternalServices.PaymentGateway;
using Stripay.API.Models;
using Stripay.API.Persistence;

namespace Stripay.API.Controllers;

[Route("api/v1.0/payments")]
public class PaymentController(IPaymentService paymentService, AppDbContext appDbContext, ILogger<PaymentController> logger) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;
    private readonly ILogger<PaymentController> _logger = logger;
    private readonly AppDbContext _context = appDbContext;

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
                
            var paymentResponse = await _paymentService.ProcessPaymentAsync(new ProcessPaymentRequest
            {
                Amount = (long)(request.Amount * 100),
                Currency = "USD",
                CapturePayment = true,
                PaymentMethodTypes = ["card"],
                PaymentMethodId = request.PaymentMethodId,
                CustomerEmail = request.CustomerEmail,
                CustomerName = request.CardName
            });

            var payment = new Payment
            {
                PaymentMethodId = paymentResponse.PaymentMethodId,
                Amount = request.Amount,
                CardName = request.CardName,
                CardExpiryMonth = request.CardExpiryMonth,
                CardExpiryYear = request.CardExpiryYear,
                CardLast4Digits = request.CardLast4Digits,
                ChargeId = paymentResponse.ChargeId,
                StripeCustomerId = paymentResponse.CustomerId,
                PaymentStatus = PaymentStatus.Payed,
                CreatedOn = DateTime.UtcNow
             };

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the payment.");
            return BadRequest(ex.Message);
        }
    }

}

