using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Repository;
using AutoMapper;

namespace Armut.MS.Service.Chat;

public class ChatService : IChatService
{
    private readonly IMongoRepository<Messages> _messagesRepository;
    private readonly IAuthUserInformation _authUserInformation;

    public ChatService(IMapper mapper, IMongoRepository<Messages> messagesRepository, IAuthUserInformation authUserInformation)
    {
        this._messagesRepository = messagesRepository;
        this._authUserInformation = authUserInformation;
    }
}

