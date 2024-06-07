using Microsoft.AspNetCore.Mvc;
using pruebatecnica.Application.Interfaces;
using pruebatecnica.Domain.DTOs;

namespace pruebatecnica.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
    {
        var payments = await _paymentRepository.GetAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentDto>> GetPayment(int id)
    {
        var payment = await _paymentRepository.GetById(id);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentDto>> CreatePayment(PaymentDto paymentDto)
    {
        try
        {
            var createdPayment = await _paymentRepository.CreateAsync(paymentDto);
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.PaymentId }, createdPayment);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Error: {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PaymentDto>> UpdatePayment(int id, PaymentDto paymentDto)
    {
        if (id != paymentDto.PaymentId)
        {
            return BadRequest("Payment ID mismatch");
        }

        var updatedPayment = await _paymentRepository.UpdateAsync(id, paymentDto);
        if (updatedPayment == null)
        {
            return NotFound($"Payment with Id '{id}' not found");
        }

        return Ok(updatedPayment);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePayment(int id)
    {
        var existingPayment = await _paymentRepository.GetById(id);
        if (existingPayment == null)
        {
            return NotFound($"Payment with Id '{id}' not found");
        }

        await _paymentRepository.DeleteAsync(id);
        return NoContent();
    }
}