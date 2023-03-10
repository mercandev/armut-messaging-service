using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Helper;
using Armut.MS.Infrastructure.Jwt;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.SharedObjects.Login;
using Serilog;

namespace Armut.MS.Service.Login;

public class LoginService : ILoginService
{
    private readonly IJwtSecurity _jwtSecurity;
    private readonly IMongoRepository<Users> _usersRepository;
    private readonly ILogger _logger;

    public LoginService(IJwtSecurity jwtSecurity , IMongoRepository<Users> usersRepository , ILogger logger)
    {
        this._jwtSecurity = jwtSecurity;
        this._usersRepository = usersRepository;
        this._logger = logger;
    }

    public async Task<string> LoginUser(LoginViewModel model)
    {
        var userResult = await _usersRepository
            .FindOneAsync(x => x.Username == model.Username && x.Password == model.Password && x.IsActive);
        
        if (userResult is null)
        {
            _logger.Error("User not found!");
            throw new ApplicationException("User not found!");
        }

        userResult.LastLoginDate = DateTime.Now;
        await _usersRepository.ReplaceOneAsync(userResult);
        _logger.Information($"User loggin success! UserId:{userResult.Id} - LoginDate: {userResult.LastLoginDate}");

        return _jwtSecurity.CreateJwtToken(ClaimsHelper.CreateSourceClaims(userResult.Username, Convert.ToString(userResult.Id)));
    }
}

