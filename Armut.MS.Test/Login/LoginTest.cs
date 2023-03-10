using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Jwt;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.Service.Login;
using Armut.MS.Service.User;
using Armut.MS.SharedObjects.Login;
using Armut.MS.SharedObjects.User;
using AutoMapper;
using Microsoft.AspNetCore.Components.Routing;
using Moq;
using Serilog;

namespace Armut.MS.Test.Login;

public class LoginTest
{
    private readonly LoginService loginService;
    private readonly Mock<ILoginService> _loginService = new();
    private readonly Mock<IJwtSecurity> _jwtSecurity = new();
    private readonly Mock<IMongoRepository<Users>> _userMongoService = new();
    private readonly Mock<ILogger> _logger = new();

    public LoginTest()
    {
        loginService = new LoginService(_jwtSecurity.Object, _userMongoService.Object,_logger.Object);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenUserNotFound()
    {
        var result = await Assert.ThrowsAsync<ApplicationException>(() => loginService.LoginUser(new LoginViewModel { Username= "test" , Password="test"}));

        //Assert
        Assert.Equal("User not found!", result.Message);
    }

    //[Fact] //Internal Test
    //public async Task ShouldUserLoginWithTrueData()
    //{
    //    //Fact
    //    var result = await loginService.LoginUser(new LoginViewModel { Username = "kaan", Password = "kaan" });

    //    //Assert
    //    Assert.StartsWith("ey", result);
    //}
}

