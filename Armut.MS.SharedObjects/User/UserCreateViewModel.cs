using System;
using System.ComponentModel.DataAnnotations;
using Armut.MS.Infrastructure.Validation;

namespace Armut.MS.SharedObjects.User;

public sealed record UserCreateViewModel
{
    [CustomArmutValidationAttribute]
    public string Name { get; set; }

    [CustomArmutValidationAttribute]
    public string Surname { get; set; }

    [CustomArmutValidationAttribute]
    public string Username { get; set; }

    [CustomArmutValidationAttribute]
    public string Password { get; set; }

    [CustomArmutValidationAttribute]
    public string Email { get; set; }
}