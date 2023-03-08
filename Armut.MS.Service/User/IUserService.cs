using System;
using Armut.MS.Domain.Model;
using Armut.MS.SharedObjects.User;

namespace Armut.MS.Service.User;

public interface IUserService
{
    Task<bool> CreateUser(UserCreateViewModel model);
    Task<bool> BanUser(string username);
}
