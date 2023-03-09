using System;
using Armut.MS.SharedObjects.Chat;
using Microsoft.AspNetCore.Http;

namespace Armut.MS.Service.Chat;

public interface IChatService
{
    Task CreateChat(string username, HttpContext httpContext);
}

