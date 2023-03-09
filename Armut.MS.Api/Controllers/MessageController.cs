using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Armut.MS.Infrastructure.Validation;
using Armut.MS.Service.Chat;
using Armut.MS.Service.Message;
using Armut.MS.SharedObjects.Message;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Armut.MS.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService) => this._messageService = messageService;

    [HttpGet]
    public List<ChatRoomListViewModel> GetChatRoomList()
    =>  _messageService.ChatRoomList();

    [HttpGet]
    public List<MessageHistoryListViewModel> GetMessageList([CustomArmutValidation] string chatId)
    => _messageService.MessageListViaChatId(chatId);
}