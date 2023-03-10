using System;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Const;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.SharedObjects.Message;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Armut.MS.Service.Message;

public class MessageService : IMessageService
{
    private readonly IMongoRepository<Messages> _messagesRepository;
    private readonly IMongoRepository<Chats> _chatsRepository;
    private readonly IAuthUserInformation _authUserInformation;
    private readonly ILogger _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public MessageService(
        IMongoRepository<Messages> messagesRepository,
        IMongoRepository<Chats> chatsRepository,
        IAuthUserInformation authUserInformation,
        ILogger logger,
        IMemoryCache memoryCache,
        IMapper mapper
        )
    {
        this._messagesRepository = messagesRepository;
        this._authUserInformation = authUserInformation;
        this._logger = logger;
        this._memoryCache = memoryCache;
        this._chatsRepository = chatsRepository;
        this._mapper = mapper;
    }

    public List<ChatRoomListViewModel> ChatRoomList()
    {
        var cacheResult = _memoryCache.Get<List<Chats>>(string.Format(ChatConst.CHAT_HISTORY_KEY, _authUserInformation.UserId));

        if (cacheResult is not null)
        {
            return _mapper.Map<List<ChatRoomListViewModel>>(cacheResult);
        }

        var chatsHistory = _chatsRepository.AsQueryable().Where(x => x.OwnerId == MongoDB.Bson.ObjectId.Parse(_authUserInformation.UserId)).ToList();

        if (!chatsHistory.Any())
        {
            _logger.Error($"Chats not found! - Query userId: {_authUserInformation.UserId}");
            throw new ApplicationException("Chats not found!");
        }

        var mapperResult = _mapper.Map<List<ChatRoomListViewModel>>(chatsHistory);

        _memoryCache.Set<List<ChatRoomListViewModel>>(string.Format(ChatConst.CHAT_HISTORY_KEY,_authUserInformation.UserId), mapperResult, TimeSpan.FromSeconds(5));

        return mapperResult;
    }

    public List<MessageHistoryListViewModel> MessageListViaChatId(string chatId)
    {
        var cacheResult = _memoryCache.Get<List<MessageHistoryListViewModel>>(string.Format(ChatConst.MESSAGES_HISTORY_KEY, chatId, _authUserInformation.UserId));

        if (cacheResult is not null)
        {
            return _mapper.Map<List<MessageHistoryListViewModel>>(cacheResult);
        }

        var messageList = _messagesRepository.AsQueryable().Where(x => x.ChatId == MongoDB.Bson.ObjectId.Parse(chatId)).ToList();

        if (!messageList.Any())
        {
            _logger.Error($"Messages not found! - Query userId: {_authUserInformation.UserId}");
            throw new ApplicationException("Messages not found!");
        }

        var mapperResult = _mapper.Map<List<MessageHistoryListViewModel>>(messageList);

        _memoryCache.Set<List<MessageHistoryListViewModel>>(string.Format(ChatConst.MESSAGES_HISTORY_KEY, chatId ,_authUserInformation.UserId), mapperResult, TimeSpan.FromSeconds(10));

        return mapperResult;
    }
}