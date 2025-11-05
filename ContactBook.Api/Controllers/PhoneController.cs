using Microsoft.AspNetCore.Mvc;
using ContactBook.Application.Interfaces.Services;
using ContactBook.Application.DTOs.Phone;
using ContactBook.Api.Extensions;

namespace ContactBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhoneController : ControllerBase
{
    private readonly IPhoneService _phoneService;

    public PhoneController(IPhoneService phoneService)
    {
        _phoneService = phoneService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _phoneService.GetPhoneByIdAsync(id, cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _phoneService.GetAllPhonesAsync(cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePhoneDto dto, CancellationToken cancellationToken)
    {
        var result = await _phoneService.AddPhoneAsync(dto, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePhoneDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest("Phone ID in route and body do not match.");

        var result = await _phoneService.UpdatePhoneAsync(id, dto, cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _phoneService.DeletePhoneAsync(id, cancellationToken);
        return result.ToActionResult();
    }
}
