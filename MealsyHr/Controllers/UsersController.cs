using MealsyHr.Models;
using MealsyHr.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealsyHr.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userService.AddUser(user);

        return Ok(user);
    }
}
