using System;
using Armut.MS.SharedObjects.Login;

namespace Armut.MS.Service.Login;

public interface ILoginService
{
    Task<string> LoginUser(LoginViewModel model);
}
