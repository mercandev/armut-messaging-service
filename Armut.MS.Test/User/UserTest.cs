using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.Service.Login;
using Armut.MS.Service.Mapping;
using Armut.MS.Service.User;
using Armut.MS.SharedObjects.Login;
using Armut.MS.SharedObjects.User;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Armut.MS.Test.User;

public class UserTest 
{
    private readonly UserService userService;
    private readonly Mock<IUserService> _userService = new();
    private readonly Mock<IMapper> _mapperService = new();
    private readonly Mock<IMongoRepository<Users>> _userMongoService = new();
    private readonly Mock<IAuthUserInformation> _authInformationService = new();

    public UserTest()
    {
        userService = new UserService(_mapperService.Object,_userMongoService.Object,_authInformationService.Object);
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenUserNotFound()
    {
        var result = await Assert.ThrowsAsync<ApplicationException>(() => userService.BanUser("kaan"));

        //Assert
        Assert.Equal("User not found!", result.Message);
    }
}


