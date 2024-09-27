using Microsoft.AspNetCore.Mvc;
using SF.BikeTheft.Application.Interfaces;
using SF.BikeTheft.Application.Models.Requests;
using SF.BikeTheft.Application.Models.Responses;

namespace SF.BikeTheft.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserRes>> CreateUser(CreateUserReq user)
    {
        var result = await _userService.CreateUser(user);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ValidateUserRes>> ValidateUser(ValidateUserReq req)
    {
        var result = await _userService.ValidateUser(req);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<GetAllActiveUsersRes>> GetAllActiveUsers()
    {
        var result = await _userService.GetAllActiveUsers();
        return Ok(result);
    }
}
