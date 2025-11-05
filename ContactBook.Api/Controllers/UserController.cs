using Microsoft.AspNetCore.Mvc;
using ContactBook.Application.Interfaces.Services;
using ContactBook.Shared.DTOs.User;
using ContactBook.Api.Extensions;

namespace ContactBook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserByIdAsync(id, cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllUsersAsync(cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _userService.AddUserAsync(dto, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest("User ID in route and body do not match.");

        var result = await _userService.UpdateUserAsync(dto, cancellationToken);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteUserAsync(id, cancellationToken);
        return result.ToActionResult();
    }
}
