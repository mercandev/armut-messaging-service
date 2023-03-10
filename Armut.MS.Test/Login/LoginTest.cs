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
    public async Task ShouldThrowExceptionWhenLoginViewModelUsernameNullSend()
    {
        var input = new LoginViewModel { Username = "" , Password = "kaan"};
        var dependencyMock = new Mock<ILoginService>();
        dependencyMock.Setup(d => d.LoginUser(new LoginViewModel { }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => loginService.LoginUser(input));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenLoginViewModelPasswordNullSend()
    {
        var input = new LoginViewModel { Username = "Kaan", Password = "" };
        var dependencyMock = new Mock<ILoginService>();
        dependencyMock.Setup(d => d.LoginUser(new LoginViewModel { }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => loginService.LoginUser(input));
    }
}

