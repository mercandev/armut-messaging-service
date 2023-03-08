using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Armut.MS.Service.Login;
using Armut.MS.Service.User;
using Armut.MS.SharedObjects.Login;
using Armut.MS.SharedObjects.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Armut.MS.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LoginController : Controller
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    => this._loginService = loginService;

    [HttpPost]
    [AllowAnonymous]
    public async Task<string> Login(LoginViewModel model)
    => await _loginService.LoginUser(model);
}

