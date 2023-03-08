using System;
using System.ComponentModel.DataAnnotations;
using Armut.MS.Infrastructure.Validation;

namespace Armut.MS.SharedObjects.Login;

public sealed record class LoginViewModel
{
    [CustomArmutValidation]
    public string Username { get; set; }

    [CustomArmutValidation]
    public string Password { get; set; }
}