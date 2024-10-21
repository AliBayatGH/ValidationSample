using MevService.Models;
using MevService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MevService.Controllers;


[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("save")]
    public IActionResult SaveUser([FromBody] User user)
    {
        _userService.SaveUser(user);

        return Ok("User saved with Pending status.");
    }

    [HttpGet("pending")]
    public IActionResult GetPendingUsers()
    {
        var pendingUsers = _userService.GetPendingUsers();

        return Ok(pendingUsers);
    }

    [HttpPost("update/{id}")]
    public IActionResult UpdateUserStatus(int id, [FromBody] short status)
    {
        _userService.UpdateUserStatus(id, status);

        return Ok("User status updated.");
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUser(id);
        if (user == null) return NotFound();

        return Ok(user);
    }
}
