using System;
using Armut.MS.SharedObjects.Message;

namespace Armut.MS.Service.Message;

public interface IMessageService
{
	List<ChatRoomListViewModel> ChatRoomList();
	List<MessageHistoryListViewModel> MessageListViaChatId(string chatId);
}

