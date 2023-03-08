using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Armut.MS.Infrastructure.Jwt;

public class JwtSecurity : IJwtSecurity
{
    private readonly JwtModel _jwtModel;

    public JwtSecurity(JwtModel jwtModel) => this._jwtModel = jwtModel;
    
    public string CreateJwtToken(Claim[] claims)
    {
        var token = new JwtSecurityToken
        (
            issuer: _jwtModel.Issuer,
            audience: _jwtModel.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.Key)),SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}