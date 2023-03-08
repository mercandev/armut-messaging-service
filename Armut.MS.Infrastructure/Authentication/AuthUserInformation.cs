using System;
using Microsoft.AspNetCore.Http;

namespace Armut.MS.Infrastructure.Authentication;

public class AuthUserInformation : IAuthUserInformation
{
    public readonly IHttpContextAccessor _httpContextAccessor;

    public AuthUserInformation()
    {

    }

    public AuthUserInformation(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
        this.UserId = _httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type.Equals("UserId")).Select(x => x.Value).FirstOrDefault();
        this.Username = _httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type.Equals("Username")).Select(x => x.Value).FirstOrDefault();
    }

    public string UserId { get; set; }

    public string Username { get; set; }
}
