using Microsoft.AspNetCore.Mvc;
using PaymentAPI.DTOs;
using PaymentAPI.Services.Interfaces;

namespace PaymentAPI.Controllers;

[ApiController]
[Route("api/payment-requests")]
public class PaymentRequestsController : ControllerBase
{
    private readonly IPaymentRequestService _service;

    public PaymentRequestsController(IPaymentRequestService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequestDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetAll), new { }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }
}
