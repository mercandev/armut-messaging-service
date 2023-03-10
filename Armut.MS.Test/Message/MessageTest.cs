using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Jwt;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.Service.Login;
using Armut.MS.Service.Message;
using Armut.MS.SharedObjects.Login;
using Armut.MS.SharedObjects.Message;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Serilog;

namespace Armut.MS.Test.Message;

public class MessageTest
{
    private readonly MessageService messageService;
    private readonly Mock<IMessageService> _messageService = new();
    private readonly Mock<IAuthUserInformation> _authInformationService = new();
    private readonly Mock<IMongoRepository<Chats>> _chatsMongoService = new();
    private readonly Mock<IMongoRepository<Messages>> _messagesMongoService = new();
    private readonly Mock<IMemoryCache> _memoryCache = new();
    private readonly Mock<ILogger> _logger = new();
    private readonly Mock<IMapper> _mapperService = new();

    public MessageTest()
    {
        messageService = new MessageService(
            _messagesMongoService.Object,
            _chatsMongoService.Object,
            _authInformationService.Object,
            _logger.Object,
            _memoryCache.Object,
            _mapperService.Object
            );
    }

    [Fact] 
    public void ShouldChatRoomListReturnEmpty()
    {
        var result = messageService.ChatRoomList();

        //Assert
        Assert.Equal(new List<ChatRoomListViewModel>(), result);
    }

    [Fact]
    public void ShouldChatMessageListViaChatIdReturnEmpty()
    {
        var result = messageService.MessageListViaChatId("");

        //Assert
        Assert.Equal(new List<MessageHistoryListViewModel>(), result);
    }
}

