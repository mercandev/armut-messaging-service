using System;
using System.Text;
using Armut.MS.Infrastructure.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Armut.MS.Infrastructure.Engine;

public static class JwtRegister
{
    public static WebApplicationBuilder JwtAndSwaggerRegister(this WebApplicationBuilder app)
    {
        var config = new JwtModel
        {
            Audience = app.Configuration["Jwt:Audience"],
            Issuer = app.Configuration["Jwt:Issuer"],
            Key = app.Configuration["Jwt:Key"]
        };

        app.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = app.Configuration["Jwt:Issuer"],
                ValidAudience = app.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app.Configuration["Jwt:Key"]))
            };
        });
        app.Services.AddAuthorization();

        app.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                          Reference = new OpenApiReference
                          {
                             Id = "Bearer",
                             Type = ReferenceType.SecurityScheme
                          }
                       },
                       new List<string>()
                    }
                 });
        });

        app.Services.AddSingleton(config);

        return app;
    }
}