using System;
namespace Armut.MS.Infrastructure.Authentication;

public interface IAuthUserInformation
{
    string UserId { get; set; }
    string Username { get; set; }
}