using System;
namespace Armut.MS.SharedObjects.Message;

public sealed class ChatRoomListViewModel
{
	public string ChatId { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime EndDate { get; set; }
}