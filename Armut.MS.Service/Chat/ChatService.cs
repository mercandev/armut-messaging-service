using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using Armut.MS.Domain.Model;
using Armut.MS.Infrastructure.Authentication;
using Armut.MS.Infrastructure.Helper;
using Armut.MS.Infrastructure.Repository;
using Armut.MS.SharedObjects.Chat;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Serilog;


namespace Armut.MS.Service.Chat;

public class ChatService : IChatService
{
    private static ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
    private readonly IMongoRepository<Messages> _messagesRepository;
    private readonly IMongoRepository<Users> _usersRepository;
    private readonly IMongoRepository<Chats> _chatsRepository;
    private readonly IAuthUserInformation _authUserInformation;
    private readonly ILogger _logger;

    public ChatService(
        IMapper mapper,
        IMongoRepository<Messages> messagesRepository,
        IAuthUserInformation authUserInformation,
        IMongoRepository<Users> usersRepository,
        ILogger logger,
        IMongoRepository<Chats> chatsRepository
        )
    {
        this._messagesRepository = messagesRepository;
        this._authUserInformation = authUserInformation;
        this._usersRepository = usersRepository;
        this._logger = logger;
        this._chatsRepository = chatsRepository;
    }

    public async Task CreateChat(string username, HttpContext httpContext)
    {
        if (username.ToLower().Equals(_authUserInformation.Username))
        {
            _logger.Error($"You cannot send messages to your own username: {_authUserInformation.Username} - UserId: {_authUserInformation.UserId}");
            throw new Exception("You cannot send messages to your own username");
        }

        var checkUser = await _usersRepository.FindOneAsync(x => x.Username == username && x.IsActive);

        if (checkUser is null)
        {
            _logger.Error($"Chat not create! Because user not found!: Username: {username}");
            throw new Exception("Chat not create! Because user not found!");
        }

        var room = new Chats();
        room = await _chatsRepository.FindOneAsync(x => x.OwnerId == checkUser.Id  && x.InvitedUserId == ArmutMSHelper.BsonParserId(_authUserInformation.UserId) && x.IsActive);

        if (room is null)
        {
            CheckIsUserBanned(checkUser.BannedUserId, username);

            var newChatRoom = new Chats
            {
                OwnerId = ArmutMSHelper.BsonParserId(_authUserInformation.UserId),
                InvitedUserId = checkUser.Id
            };

            await _chatsRepository.InsertOneAsync(newChatRoom);

            //TODO: Refactor this code. Going to database twice!
            room = await _chatsRepository.FindOneAsync(x => x.OwnerId == newChatRoom.OwnerId && x.InvitedUserId == newChatRoom.InvitedUserId && x.IsActive);
        }

        if (httpContext.WebSockets.IsWebSocketRequest)
        {
            WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();

            string id = Guid.NewGuid().ToString();
            _sockets.TryAdd(id, webSocket);

            var joinChatText = $"{_authUserInformation.Username} joined the chat";

            if (room.OwnerId == ArmutMSHelper.BsonParserId(_authUserInformation.UserId))
            {
                joinChatText = $"{_authUserInformation.Username} craete chat room. Waiting for {username}";
            }

            await RecordMessage(room, joinChatText);
            await SendAsync(joinChatText, CancellationToken.None);

            await HandleWebSocket(id, webSocket, username, room);
        }
    }

    private async Task HandleWebSocket(string id, WebSocket webSocket, string username , Chats roomInformation)
    {
        byte[] buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await RemoveWebSocket(id, username , roomInformation);
            }
            else
            {
                if(await IsUserBanned(_authUserInformation.Username))
                {
                    await SendAsync($"You have been blocked by {username}! The session has ended.", CancellationToken.None);
                    await RecordMessage(roomInformation, $"You have been blocked by {username}! The session has ended.", true);

                    throw new Exception("You have been blocked by user. The session has ended!");
                }

                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                await RecordMessage(roomInformation, $"{_authUserInformation.Username}: {message}");
                await SendAsync($"{_authUserInformation.Username}: {message}", CancellationToken.None);
            }
        }

        await RemoveWebSocket(id, username , roomInformation);
    }

    private async Task RemoveWebSocket(string id, string username, Chats roomInformation)
    {
        if (_sockets.TryRemove(id, out WebSocket removedSocket))
        {
            await removedSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket closed", CancellationToken.None);

            await RecordMessage(roomInformation, $"{_authUserInformation.Username} left the chat", false);
            await SendAsync($"{_authUserInformation.Username} left the chat", CancellationToken.None);
        }
    }

    private async Task SendAsync(string message, CancellationToken cancellationToken)
    {
        foreach (WebSocket socket in _sockets.Values)
        {
            if (socket.State == WebSocketState.Open)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, cancellationToken);
            }
        }
    }


    private async Task RecordMessage(Chats roomInformation , string message , bool isActive = true)
    {
        var messages = new Messages
        {
            ChatId = roomInformation.Id,
            Content = message,
            OwnerId = roomInformation.OwnerId,
            ReceiverId = roomInformation.InvitedUserId,
            IsActive = isActive
        };

        await _messagesRepository.InsertOneAsync(messages);
    }

    private void CheckIsUserBanned(string[] bannedUserId, string username)
    {
        var result = ArmutMSHelper.CheckBannedUser(bannedUserId, _authUserInformation.UserId.ToString());

        if (result)
        {
            _logger.Error($"You cannot send messages because you have been banned by the user!: Username: {username} - AuthUser:{_authUserInformation.UserId}");
            throw new Exception("You cannot send messages because you have been banned by the user!");
        }
    }

    private async Task<bool> IsUserBanned(string username)
    {
       var userBannedList = (await _usersRepository.FindOneAsync(x => x.Username == username && x.IsActive)).BannedUserId;

       return ArmutMSHelper.CheckBannedUser(userBannedList, _authUserInformation.UserId.ToString());
    }
}

