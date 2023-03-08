using System;
using System.Security.Claims;

namespace Armut.MS.Infrastructure.Jwt;

public interface IJwtSecurity
{
    string CreateJwtToken(Claim[] claims);
}
