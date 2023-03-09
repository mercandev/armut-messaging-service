using System;
using Armut.MS.Infrastructure.Attributes;
using Armut.MS.Infrastructure.Entity;
using MongoDB.Bson;

namespace Armut.MS.Domain.Model;

[BsonCollection("message")]
public class Messages : Document
{
    public ObjectId ChatId { get; set; }
    public ObjectId OwnerId { get; set; }
    public ObjectId ReceiverId { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
}