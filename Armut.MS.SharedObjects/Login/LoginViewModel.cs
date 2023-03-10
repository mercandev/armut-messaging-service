using System;
using System.ComponentModel.DataAnnotations;
using Armut.MS.Infrastructure.Validation;

namespace Armut.MS.SharedObjects.Login;

public sealed record class LoginViewModel
{
    [CustomArmutValidationAttribute]
    public string Username { get; set; }

    [CustomArmutValidationAttribute]
    public string Password { get; set; }
}