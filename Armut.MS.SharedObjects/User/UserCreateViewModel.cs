using System;
using System.ComponentModel.DataAnnotations;
using Armut.MS.Infrastructure.Validation;

namespace Armut.MS.SharedObjects.User;

public sealed record UserCreateViewModel
{
    [CustomArmutValidation]
    public string Name { get; set; }

    [CustomArmutValidation]
    public string Surname { get; set; }

    [CustomArmutValidation]
    public string Username { get; set; }

    [CustomArmutValidation]
    public string Password { get; set; }

    [CustomArmutValidation]
    public string Email { get; set; }
}