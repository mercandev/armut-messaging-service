using System;
using Armut.MS.Infrastructure.Attributes;
using Armut.MS.Infrastructure.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Armut.MS.Domain.Model;

[BsonCollection("chats")]
public class Chats : Document
{
    public ObjectId OwnerId { get; set; }
    public ObjectId InvitedUserId { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime EndDate { get; set; }
}