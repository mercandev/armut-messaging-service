using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Helper;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.SharedObjects.User;
using AutoMapper;

namespace Armut.MS.Service.User;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IMongoRepository<Users> _usersRepository;
    private readonly IAuthUserInformation _authUserInformation;

    public UserService(IMapper mapper, IMongoRepository<Users> usersRepository, IAuthUserInformation authUserInformation)
    {
        this._mapper = mapper;
        this._usersRepository = usersRepository;
        this._authUserInformation = authUserInformation;
    }

    public async Task<bool> BanUser(string username)
    {
        if (username.Equals(_authUserInformation.Username))
        {
            throw new ApplicationException("You cannot block your own user!");
        }

        var userToBeBlock = await _usersRepository.FindOneAsync(x=> x.Username == username && x.IsActive);

        if (userToBeBlock is null)
        {
            throw new ApplicationException("User not found!");
        }

        var userToBlock = await _usersRepository.FindByIdAsync(_authUserInformation.UserId.ToString());

        if (ArmutMSHelper.CheckBannedUser(userToBlock.BannedUserId, userToBeBlock.Id.ToString()))
        {
            throw new ApplicationException("You have blocked this user before!");
        }

        userToBlock.BannedUserId = ArmutMSHelper.Add(userToBlock.BannedUserId, userToBeBlock.Id.ToString());
        await _usersRepository.ReplaceOneAsync(userToBlock);

        return true;
    }

    public async Task<bool> CreateUser(UserCreateViewModel model)
    {
        var userNameAndEmailCheck = await _usersRepository.
            FindOneAsync(x => x.Username == model.Username || x.Email == model.Email);

        if (userNameAndEmailCheck is not null)
        {
            throw new ApplicationException("This username or email has been used before");
        }

        var userModel = _mapper.Map<Users>(model);

        await _usersRepository.InsertOneAsync(userModel);

        return true;
    }
}