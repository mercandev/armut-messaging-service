using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.Service.Mapping;
using Armut.MS.Service.User;
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
    public async Task ShouldThrowExceptionWhenCreateModelNameNullSend()
    {
        var input = new UserCreateViewModel { Name = null, Surname = "Mercan", Email = "mercan@kaan.com", Password = "test", Username = "kaan" };
        var dependencyMock = new Mock<IUserService>();
        dependencyMock.Setup(d => d.CreateUser(new UserCreateViewModel { }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() =>  userService.CreateUser(input));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenCreateModelSurnameNullSend()
    {
        var input = new UserCreateViewModel { Name = "Kaan", Surname = "", Email = "mercan@kaan.com", Password = "test", Username = "kaan" };
        var dependencyMock = new Mock<IUserService>();
        dependencyMock.Setup(d => d.CreateUser(new UserCreateViewModel { Name= "Kaan" , Surname= "Mercan" , Email= "mercan@kaan.com" , Password="test" , Username="kaan" }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => userService.CreateUser(input));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenCreateModelEmailNullSend()
    {
        var input = new UserCreateViewModel { Name = "Kaan", Surname = "Mercan", Email = null, Password = "test", Username = "kaan" };
        var dependencyMock = new Mock<IUserService>();
        dependencyMock.Setup(d => d.CreateUser(new UserCreateViewModel { }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => userService.CreateUser(input));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenCreateModelPasswordNullSend()
    {
        var input = new UserCreateViewModel { Name = "Kaan", Surname = "Mercan", Email = "mercan@kaan.com", Password = null, Username = "kaan" };
        var dependencyMock = new Mock<IUserService>();
        dependencyMock.Setup(d => d.CreateUser(new UserCreateViewModel { }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => userService.CreateUser(input));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenCreateModelUsernameNullSend()
    {
        var input = new UserCreateViewModel { Name = "Kaan", Surname = "Mercan", Email = "mercan@kaan.com", Password = "test", Username = null };
        var dependencyMock = new Mock<IUserService>();
        dependencyMock.Setup(d => d.CreateUser(new UserCreateViewModel { }))
            .Throws<Exception>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => userService.CreateUser(input));
    }
}


