using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Armut.MS.Infrastructure.Validation;
using Armut.MS.Service.User;
using Armut.MS.SharedObjects.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Armut.MS.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    => this._userService = userService;

    [HttpPost]
    [AllowAnonymous]
    public async Task<bool> CreateUser(UserCreateViewModel model)
    => await _userService.CreateUser(model);

    [HttpGet]
    public async Task<bool> BanUser([CustomArmutValidation] string username)
    => await _userService.BanUser(username);

    [HttpGet]
    [AllowAnonymous]
    public IActionResult HealtCheck()
    => Ok("Api is a live! :)");
}
