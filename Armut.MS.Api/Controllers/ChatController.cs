using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Armut.MS.Infrastructure.Validation;
using Armut.MS.Service.Chat;
using Armut.MS.SharedObjects.Message;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Armut.MS.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ChatController : Controller
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService) => this._chatService = chatService;

    [HttpGet]
    [Authorize]
    public async Task StartChat(string username) => await _chatService.CreateChat(username , HttpContext);
}

