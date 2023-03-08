using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Engine;
using Armut.MS.Infrastructure.Jwt;
using Armut.MS.Service.Login;
using Armut.MS.Service.Mapping;
using Armut.MS.Service.User;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.JwtAndSwaggerRegister();
builder.Services.RegisterMongoDB(configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperRegister).Assembly);
builder.RegisterSerilogAndElasticSearch(configuration);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtSecurity, JwtSecurity>();
builder.Services.AddTransient<IAuthUserInformation, AuthUserInformation>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddCors(p => p.AddPolicy("CorsApp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseWebSockets();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerRegister();

app.UseAuthorization();

app.UseCors("CorsApp");

app.MapControllers();

app.Run();

