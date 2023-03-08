using System;
using System.Security.Claims;
using Armut.MS.Infrastructure.Const;

namespace Armut.MS.Infrastructure.Helper;

public static class ClaimsHelper
{
    public static Claim[] CreateSourceClaims(string userName, string userId)
    {
        return new[]
        {
            new Claim(ClaimsConst.USERNAME, userName),
            new Claim(ClaimsConst.USERID, userId),
        };
    }
}